using AntropoPollWebApi.Core.Contexts;
using AntropoPollWebApi.Core.Models;
using AntropoPollWebApi.Core.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.VisualBasic;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace AntropoPollWebApi.Core.Services
{
    public class CalcService
    {
        private AntropoPollSettings _options;
        private TelegramBotClient _bot;

        public CalcService(IOptions<AntropoPollSettings> options)
        {
            _options = options.Value;
            _bot = new TelegramBotClient(_options.BotToken);
        }

        public async System.Threading.Tasks.Task<int> CalcResultAsync()
        {
            //Вытащим все результаты и обновим статус

            using var context = new AntropoPollContext(_options.AntropoPollProviders);

            if (!context.Results.Any(x => x.Status == ResultStatus.Saved))
                return 0;


            var results = await context.Results.Include(x => x.ResultQuestions).ThenInclude(x => x.Answer)
                .Include(x => x.ResultQuestions).ThenInclude(x => x.BaseQuestion).Include(x => x.Event).ThenInclude(x => x.Schema)
                .Where(x => x.Status == ResultStatus.Saved).ToListAsync();

            var messages = new List<string>();

            foreach (var result in results)
            {
                var reportStopwatch = Stopwatch.StartNew();

                var systemVariables = await context.SchemaVariables.Where(x => x.SchemaId == result.Event.SchemaId).ToListAsync();
                var schemeStanValue = result.Event.Schema.StanValue;



                var systemVariableReport = systemVariables.Select(x => new SystemVariableReport()
                {
                    Guid = Guid.NewGuid(),
                    LastUpdate = DateTime.UtcNow,
                    InviteId = result.InviteId,
                    SchemaVariableId = x.Guid,
                    Value = 0,
                    StanValue = 0,
                    MaxValue = 0
                }).ToList();

                //Вычисление...
                foreach (var question in result.ResultQuestions)
                {
                    var variable = systemVariableReport?.FirstOrDefault(x =>
                        x.SchemaVariableId == question.BaseQuestion.SchemaVariableId);

                    if (variable != null)
                    {
                        variable.Value += question.Answer.VariableValue.GetValueOrDefault(0);
                    }
                }

                var variableIds = systemVariableReport.Select(x => x.SchemaVariableId).ToList();

                var questionsScheme = await context.BaseQuestions.Where(x => x.SchemaId == result.Event.SchemaId).
                        Where(x => variableIds.Contains(x.SchemaVariableId.Value)).Include(x => x.Answers)
                        .ToListAsync();

                //Для каждой переменной посчитаем макимальное значение и переведем в стены
                foreach (var systemVariable in systemVariableReport)
                {
                    var questions = questionsScheme.Where(x => x.SchemaVariableId == systemVariable.SchemaVariableId).ToList();

                    foreach (var question in questions)
                    {
                        var maxValueQuestion = question.Answers.Max(x => x.VariableValue);
                        if (maxValueQuestion.HasValue)
                            systemVariable.MaxValue += maxValueQuestion.Value;
                    }

                    if (systemVariable.Value != 0)
                    {
                        systemVariable.StanValue = systemVariable.Value / (systemVariable.MaxValue / schemeStanValue);
                        systemVariable.StanValue = Math.Round(systemVariable.StanValue, 0);
                    }
                }

                // Получим все интерпертации для теста
                var interpretations = await context.Interpretations.Where(x => x.IsActive)
                    .Include(x => x.VariableInInterpretations)
                    .Where(x => x.SchemaId == result.Event.SchemaId)
                    .ToListAsync();

                var resultInterpretations = new List<ResultInterpretation>();

                foreach (var interpretation in interpretations)
                {
                    var countVariable = 0;
                    var variableInterpretation = interpretation.VariableInInterpretations.GroupBy(o => o.SchemaVariableId)
                        .ToDictionary(g => g.Key, g => g.ToList());

                    // Пройдем по переменным 
                    foreach (var variableInInterpretation in variableInterpretation)
                    {
                        //Найдем переменную в в результатах

                        var resultVariable = systemVariableReport.FirstOrDefault(x =>
                            x.SchemaVariableId == variableInInterpretation.Key);

                        if (resultVariable == null)
                            break;

                        foreach (var variable in variableInInterpretation.Value)
                        {
                            if ((resultVariable.StanValue >= variable.ValueMin) &&
                                (resultVariable.StanValue <= variable.ValueMax))
                            {
                                countVariable += 1;
                                // Пропускаем переменную
                                break;
                            }
                        }
                    }

                    if (countVariable == variableInterpretation.Count)
                    {
                        resultInterpretations.Add(new ResultInterpretation()
                        {
                            InterpretationId = interpretation.Guid,
                            ResultId = result.Guid,
                            LastUpdate = DateTime.UtcNow
                        });
                    }
                }


                var oldSystemVariableReport =
                    context.SystemVariableReports.Where(x => x.InviteId == result.InviteId);

                var oldInterpretation =
                   context.ResultInterpretations.Where(x => x.ResultId == result.Guid);

                if (oldSystemVariableReport.Any())
                    context.SystemVariableReports.RemoveRange(oldSystemVariableReport);

                if (oldInterpretation.Any())
                    context.ResultInterpretations.RemoveRange(oldInterpretation);

                context.SystemVariableReports.AddRange(systemVariableReport);

                if (resultInterpretations.Any())
                    context.ResultInterpretations.AddRange(resultInterpretations);

                result.Status = ResultStatus.Completed;
                result.LastUpdate = DateTime.UtcNow;

                context.Results.Update(result);
                await context.SaveChangesAsync();
                reportStopwatch.Stop();
                var message =
                    $"Result test: {result.Event.Schema.Name} completed Invited: {result.InviteId}, ID: {result.Guid}, time {reportStopwatch.Elapsed}";
                Console.WriteLine(message);

                var telegramMsg = $"Test: {result.Event.Schema.Name} - {reportStopwatch.Elapsed:hh\\:mm\\:ss\\:fff} \nInviteId: {result.InviteId} \nResultId: {result.Guid} \n";
                messages.Add(telegramMsg);
            }

            for (var i = 0; i < messages.Count; i += 10)
            {
                var msg = string.Join("\n", messages.Skip(i).Take(10));
                if (messages.Count < 10)
                    msg += $"\n Total result: {messages.Count}";

                else if  ((i > 0) && ((messages.Count / (i + 10)) == 0))
                    msg += $"\n Total result: {messages.Count}";

                await _bot.SendTextMessageAsync(_options.ChatIdPipeLine, msg).ConfigureAwait(false);
                Thread.Sleep(2000);
            }


            //context.Results.UpdateRange(results);
            //return context.SaveChanges();
            return 0;
        }


    }
}
