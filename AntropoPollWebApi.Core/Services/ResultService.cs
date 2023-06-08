using AntropoPollWebApi.Core.Contexts;
using AntropoPollWebApi.Core.Models;
using AntropoPollWebApi.Core.RequestModel;
using AntropoPollWebApi.Core.ResponseModel;
using AntropoPollWebApi.Core.Settings;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AntropoPollWebApi.Core.Services
{
    public class ResultService
    {
        private readonly IMapper _mapper;
        private readonly AntropoPollSettings _options;

        public ResultService(IMapper mapper, IOptions<AntropoPollSettings> options)
        {
            _mapper = mapper;
            _options = options.Value;
        }
        public ResultView AddResult(AddResultRequest request)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                var invite = context.Invites.Include(x => x.Event).FirstOrDefault(x => x.Guid == request.InviteId);


                if (invite == null)
                    throw new Exception($"Invite not found: {request.InviteId}");

                var newResult = _mapper.Map<Result>(request);
                newResult.LastUpdate = DateTime.UtcNow;
                newResult.EventId = invite.EventId;

                context.Add(newResult);
                //Добавим сохранение ответов 
                var resultQuestion = request.AddResultQuestion.Select(x => _mapper.Map<ResultQuestion>(x)).ToList();
                resultQuestion.ForEach(x =>
               {
                   x.ResultId = newResult.Guid;
                   x.LastUpdate = DateTime.UtcNow;
               });

                if (resultQuestion.Any())
                    context.AddRange(resultQuestion);

                context.SaveChanges();
                //var results = context.Results.Select(x => _mapper.Map<ResultView>(x)).ToList();
                return _mapper.Map<ResultView>(newResult);
            }
        }

        public IEnumerable<ResultView> GetResultList(Guid? eventId)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                var resultQuery = context.Results.OrderBy(x => x.LastUpdate).AsQueryable();
                if (eventId.HasValue)
                    resultQuery = resultQuery.Where(x => x.EventId == eventId.Value);

                var results = resultQuery.Select(x => _mapper.Map<ResultView>(x)).ToList();
                return results;
            }
        }

        public ResultDetailsView GetResultDetails(Guid resultId)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                var result = context.Results
                    .Include(x => x.ResultQuestions).ThenInclude(x => x.Answer)
                    .Include(x => x.ResultQuestions).ThenInclude(x => x.BaseQuestion)
                    .Include(x=>x.ResultTemplates)
                    .FirstOrDefault(x => x.Guid == resultId);

                if (result == null)
                    return null;

                var resultInterpretations = context.ResultInterpretations.Where(x => x.ResultId == resultId)
                    .Include(x => x.Interpretation).ThenInclude(x => x.VariableInInterpretations).ToList();

                var resultView = _mapper.Map<ResultDetailsView>(result);

                var systemVariableReport = context.SystemVariableReports.Where(x => x.InviteId == result.InviteId).Include(x => x.SchemaVariable)
                    .Select(x => _mapper.Map<SystemVariableReportView>(x)).ToList();

                resultView.SystemVariableReportView = systemVariableReport;

                if (resultInterpretations.Any())
                {
                    resultView.InterpretationView = new List<InterpretationView>();
                    foreach (var interpretation in resultInterpretations)
                    {
                        var interpretationView = _mapper.Map<InterpretationView>(interpretation.Interpretation);
                        interpretationView.VariableInInterpretationViews = interpretation.Interpretation
                            .VariableInInterpretations.Select(x => _mapper.Map<VariableInInterpretationView>(x)).ToList();
                        resultView.InterpretationView.Add(interpretationView);
                    }
                }

                if (result.ResultTemplates.Any())
                    resultView.ResultTemplateViews =
                        result.ResultTemplates.Select(x => _mapper.Map<ResultTemplateView>(x)).ToList();

                return resultView;
            }
        }

        public void ReEvalResult(Guid resultId)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                var result = context.Results.FirstOrDefault(x => x.Guid == resultId);

                if (result == null)
                    return;

                result.Status = ResultStatus.Saved;
                result.LastUpdate = DateTime.UtcNow;

                context.Update(result);
                context.SaveChanges();
            }
        }

        public ResultDetailsView UpdateDataResultReport(Guid resultId, UpdateDataReportRequest request)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                var result = context.Results.FirstOrDefault(x => x.Guid == resultId);

                if (result == null)
                    return null;

                result.Status = ResultStatus.Editing;
                result.LastUpdate = DateTime.UtcNow;

                var resultTemplate = context.ResultTemplates
                    .FirstOrDefault(x => (x.ResultId == resultId) && (x.ReportTemplateId == request.ReportTemplateId));

                if (resultTemplate == null)
                {
                    resultTemplate = new ResultTemplate()
                    {
                        LastUpdate = DateTime.UtcNow,
                        ReportTemplateId = request.ReportTemplateId,
                        ResultId = resultId,
                        TemplateData = request.TemplateData
                    };
                    context.ResultTemplates.Add(resultTemplate);
                }
                else
                {
                    resultTemplate.TemplateData = request.TemplateData;
                    resultTemplate. LastUpdate = DateTime.UtcNow;
                    context.ResultTemplates.Update(resultTemplate);
                }

                context.Update(result);
                context.SaveChanges();
            }

            return GetResultDetails(resultId);
        }

        public ResultDetailsView CompletedResultReport(Guid resultId)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                var result = context.Results.FirstOrDefault(x => x.Guid == resultId);

                if (result == null)
                    return null;

                result.Status = ResultStatus.Completed;
                result.LastUpdate = DateTime.UtcNow;

                context.Update(result);
                context.SaveChanges();
            }

            return GetResultDetails(resultId);
        }

        public ResultTemplateView GetResultTemplateDetails(Guid resultTemplateId)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                var result = context.ResultTemplates.FirstOrDefault(x => x.Guid == resultTemplateId);

                if (result == null)
                    return null;

                return _mapper.Map<ResultTemplateView>(result);
            }
        }
    }
}
