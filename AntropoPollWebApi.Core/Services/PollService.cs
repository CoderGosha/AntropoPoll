using AntropoPollWebApi.Core.Contexts;
using AntropoPollWebApi.Core.Interfaces;
using AntropoPollWebApi.Core.Models;
using AntropoPollWebApi.Core.RequestModel;
using AntropoPollWebApi.Core.ResponseModel;
using AntropoPollWebApi.Core.Services.Questions;
using AntropoPollWebApi.Core.Settings;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AntropoPollWebApi.Core.Services
{
    public class PollService : IPoll
    {
        private readonly ClosedQuestionService _closedQuestionService;

        public PollService(ClosedQuestionService closedQuestionService, IMapper mapper, IOptions<AntropoPollSettings> options, ResultService resultService)
        {
            _closedQuestionService = closedQuestionService;
            _mapper = mapper;
            _resultService = resultService;
            _options = options.Value;
        }

        private readonly IMapper _mapper;
        private readonly ResultService _resultService;
        private AntropoPollSettings _options;

        private QuestionView ConvertToView(BaseQuestion question)
        {
            var questionView = _mapper.Map<QuestionView>(question);

            switch (question.QuestionType)
            {
                case QuestionType.ClosedQuestion:
                    {
                        questionView.ClosedQuestionView = _mapper.Map<ClosedQuestionView>(question);
                        questionView.ClosedQuestionView.ClosedQuestionAnswerView =
                            _mapper.Map<List<ClosedQuestionAnswerView>>(question.Answers);
                        break;
                    }
            }

            return questionView;
        }

        private QuestionInviteView ConvertToInviteView(BaseQuestion question)
        {
            var questionView = _mapper.Map<QuestionInviteView>(question);

            switch (question.QuestionType)
            {
                case QuestionType.ClosedQuestion:
                    {
                        questionView.ClosedQuestionView = _mapper.Map<ClosedQuestionByInviteView>(question);
                        questionView.ClosedQuestionView.ClosedQuestionAnswerView =
                            _mapper.Map<List<ClosedQuestionAnswerByInviteView>>(question.Answers);
                        break;
                    }
            }

            return questionView;
        }

        public IEnumerable<QuestionView> GetQuestionList(Guid? schemeId, bool includeArchive)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                var questionsQuery = context.BaseQuestions.Include(x => x.Answers).AsQueryable();
                if (schemeId.HasValue)
                    questionsQuery = questionsQuery.Where(x => x.SchemaId == schemeId.Value);

                if (!includeArchive)
                    questionsQuery = questionsQuery.Where(x => x.IsActive);

                var questionsView = new List<QuestionView>();

                foreach (var question in questionsQuery.ToList())
                {
                    questionsView.Add(ConvertToView(question));
                }

                return questionsView;
            }
        }

        public QuestionView AddQuestion(AddQuestionRequest addQuestionRequest)
        {
            if (addQuestionRequest == null)
                throw new Exception("AddQuestionRequest is null");

            switch (addQuestionRequest.QuestionType)
            {
                case QuestionType.ClosedQuestion:
                    return _closedQuestionService.AddQuestion(addQuestionRequest);
                case QuestionType.MatchingQuestion:
                    throw new NotImplementedException("Type is not supported");
                case QuestionType.OpenQuestion:
                    throw new NotImplementedException("Type is not supported");
                case QuestionType.QuestionForRanking:
                    throw new NotImplementedException("Type is not supported");
                case QuestionType.QuestionWithScale:
                    throw new NotImplementedException("Type is not supported");
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public SchemaView AddSchema(AddOrUpdateSchemaRequest request)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                var scheme = context.Schemes.FirstOrDefault(x => x.Name == request.Name);
                if (scheme != null)
                    throw new DbUpdateException($"Name: {request.Name} is use");

                var newScheme = _mapper.Map<Schema>(request);
                newScheme.LastUpdate = DateTime.UtcNow;

                context.Schemes.Add(newScheme);
                context.SaveChanges();

                return _mapper.Map<SchemaView>(newScheme);
            }
        }

        public IEnumerable<SchemaView> GetSchemeList(bool? includeArchive)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                var schemesQuery = context.Schemes.AsQueryable();

                if ((includeArchive.HasValue) && (includeArchive.Value))
                { }
                else
                    schemesQuery = schemesQuery.Where(x => x.IsActive);

                return schemesQuery.ToList().Select(x => _mapper.Map<SchemaView>(x));
            }
        }

        public SchemaView UpdateSchema(Guid schemaId, AddOrUpdateSchemaRequest request)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                var scheme = context.Schemes.FirstOrDefault(x => x.Guid == schemaId);
                if (scheme == null)
                    return null;

                if (context.Schemes.FirstOrDefault(x => x.Name == request.Name) != null)
                    throw new DbUpdateException($"Name: {request.Name} is use");

                var newScheme = _mapper.Map(request, scheme);
                newScheme.LastUpdate = DateTime.UtcNow;

                context.Schemes.Update(newScheme);
                context.SaveChanges();

                return _mapper.Map<SchemaView>(newScheme);
            }
        }

        public ResultView AddResult(AddResultRequest request)
        {
            return _resultService.AddResult(request);
        }

        public IEnumerable<ResultView> GetResultList(Guid? eventId)
        {
            return _resultService.GetResultList(eventId);
        }

        public ResultDetailsView GetResultDetails(Guid resultId)
        {
            return _resultService.GetResultDetails(resultId);
        }

        public IEnumerable<SchemaVariableView> GetSchemaVariableList(Guid? schemeId)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                var systemVariableQuery = context.SchemaVariables.AsQueryable();
                if (schemeId.HasValue)
                    systemVariableQuery = systemVariableQuery.Where(x => x.SchemaId == schemeId.Value);

                var resultView = systemVariableQuery.ToList().Select(x => _mapper.Map<SchemaVariableView>(x));
                return resultView;
            }
        }

        public SchemaVariableView AddSchemaVariableRequest(AddSchemaVariableRequest request)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                var scheme = context.Schemes.FirstOrDefault(x => x.Guid == request.SchemaId);
                if (scheme == null)
                    throw new DbUpdateException($"Scheme: {request.SchemaId} not found");

                var variable = context.SchemaVariables.FirstOrDefault(x => x.Name == request.Name);
                if (variable != null)
                    throw new DbUpdateException($"Name: {request.Name} is use");

                var newSchemeVariable = _mapper.Map<SchemaVariable>(request);
                newSchemeVariable.LastUpdate = DateTime.UtcNow;

                context.SchemaVariables.Add(newSchemeVariable);
                context.SaveChanges();

                return _mapper.Map<SchemaVariableView>(newSchemeVariable);
            }
        }

        public List<InviteView> GetInviteList(Guid eventId)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                var invites = context.Invites.Where(x => x.EventId == eventId).Select(t => _mapper.Map<InviteView>(t)).ToList();
                var invitesIds = invites.Select(x => x.Guid);

                var results = context.Results.Where(x => invitesIds.Contains(x.InviteId)).ToList();
                foreach (var invite in invites)
                {
                    var result = results.FirstOrDefault(x => x.InviteId == invite.Guid);
                    if (result != null)
                        invite.ResultId = result.Guid;
                }

                return invites;
            }
        }

        public List<InviteView> AddInvites(AddInviteClientsRequest request)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                var invites = new List<Invite>();

                for (int index = 0; index < request.InviteCount; index++)
                {
                    invites.Add(new Invite()
                    {
                        EventId = request.EventId,
                        LastUpdate = DateTime.UtcNow
                    });
                }

                context.Invites.AddRange(invites);
                context.SaveChanges();

                return _mapper.Map<List<InviteView>>(invites);
            }
        }



        public void ReEvalResult(Guid resultId)
        {
            _resultService.ReEvalResult(resultId);
        }

        public SchemaView GetSchemaDetails(Guid id)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                var scheme = context.Schemes.FirstOrDefault(x => x.Guid == id);
                if (scheme == null)
                    return null;

                return _mapper.Map<SchemaView>(scheme);
            }
        }

        public EventView AddEvent(AddEventRequest request)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {

                var newEvent = _mapper.Map<Event>(request);
                newEvent.LastUpdate = DateTime.UtcNow;

                context.Events.Add(newEvent);
                context.SaveChanges();

                return _mapper.Map<EventView>(newEvent);
            }
        }

        public EventView GetEventDetails(Guid id)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                var detail = context.Events.FirstOrDefault(x => x.Guid == id);
                if (detail == null)
                    return null;

                return _mapper.Map<EventView>(detail);
            }
        }

        public IEnumerable<EventView> GetEventList(Guid? userId)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                var eventsQuery = context.Events.AsQueryable();
                if (userId.HasValue)
                    eventsQuery = eventsQuery.Where(x => x.UserId == userId.Value);

                var eventsView = _mapper.Map<List<EventView>>(eventsQuery.ToList());
                return eventsView;
            }
        }

        public IEnumerable<QuestionInviteView> GetQuestionListByInvite(Guid inviteId)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                var invite = context.Invites.Include(x => x.Event).FirstOrDefault(x => x.Guid == inviteId);
                if (invite == null)
                    return null;

                var questionsQuery = context.BaseQuestions.Include(x => x.Answers).AsQueryable();

                questionsQuery = questionsQuery.Where(x => x.SchemaId == invite.Event.SchemaId);

                questionsQuery = questionsQuery.Where(x => x.IsActive);

                var questionsView = new List<QuestionInviteView>();

                foreach (var question in questionsQuery.ToList())
                {
                    questionsView.Add(ConvertToInviteView(question));
                }

                return questionsView;
            }
        }

        public async Task<int> RemoveQuestion(Guid guid)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                var question = await context.BaseQuestions.FirstOrDefaultAsync(x => x.Guid == guid);
                if (question == null)
                    return 0;

                if (context.ResultQuestions.Any(x => x.BaseQuestionId == question.Guid))
                {
                    // Отпарвляем в архив
                    question.IsActive = false;
                    context.BaseQuestions.Update(question);
                }
                else
                    //Иначе удаляем 
                    context.BaseQuestions.Remove(question);

                return await context.SaveChangesAsync();

            }
        }

        public async Task<IEnumerable<InterpretationView>> GetInterpretationList(Guid? schemeId, bool? includeArchive)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                var interpretationQuery = context.Interpretations.Include(x => x.VariableInInterpretations).AsQueryable();
                if (schemeId.HasValue)
                    interpretationQuery = interpretationQuery.Where(x => x.SchemaId == schemeId.Value);

                if ((includeArchive.HasValue) && (includeArchive.Value))
                { }
                else
                    interpretationQuery = interpretationQuery.Where(x => x.IsActive);

                //var resultView = interpretationQuery.ToList().Select(x => _mapper.Map<InterpretationView>(x));
                var resultView = new List<InterpretationView>();
                foreach (var interpretation in interpretationQuery.ToList())
                {
                    var view = _mapper.Map<InterpretationView>(interpretation);
                    if (interpretation.VariableInInterpretations.Any())
                        view.VariableInInterpretationViews =
                            interpretation.VariableInInterpretations.OrderBy(x => x.SchemaVariableId).ThenBy(x => x.ValueMin).Select(x =>
                                    _mapper.Map<VariableInInterpretationView>(x)).ToList();

                    resultView.Add(view);
                }

                return resultView;
            }
        }

        public async Task<InterpretationView> AddInterpretation(AddOrUpdateInterpretationRequest request)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {

                var newInterpretation = _mapper.Map<Interpretation>(request);
                newInterpretation.LastUpdate = DateTime.UtcNow;

                if (request.VariableInInterpretationViews.Any())
                    newInterpretation.VariableInInterpretations =
                        request.VariableInInterpretationViews.Select(x => _mapper.Map<VariableInInterpretation>(x)).ToList();

                context.Interpretations.Add(newInterpretation);
                await context.SaveChangesAsync();

                return _mapper.Map<InterpretationView>(newInterpretation);
            }
        }

        public async Task<IEnumerable<ReportTemplateView>> GetReportTemplateList(Guid? schemeId)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                var templateQuery = context.ReportTemplates.AsQueryable();
                if (schemeId.HasValue)
                    templateQuery = templateQuery.Where(x => x.SchemaId == schemeId.Value);

                return templateQuery.ToList().Select(x => _mapper.Map<ReportTemplateView>(x));
            }
        }

        public async Task<ReportTemplateView> AddReportTemplate(AddOrUpdateReportTemplateRequest request)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                var newTemplate = _mapper.Map<ReportTemplate>(request);
                newTemplate.LastUpdate = DateTime.UtcNow;

                context.ReportTemplates.Add(newTemplate);
                await context.SaveChangesAsync();

                return _mapper.Map<ReportTemplateView>(newTemplate);
            }
        }

        public ResultDetailsView UpdateDataResultReport(Guid resultId, UpdateDataReportRequest request)
        {
            return _resultService.UpdateDataResultReport(resultId, request);
        }

        public ResultDetailsView CompletedResultReport(Guid resultId)
        {
            return _resultService.CompletedResultReport(resultId);
        }

        public void RemoveSchema(Guid schemaId)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                var schema = context.Schemes.FirstOrDefault(x => x.Guid == schemaId);
                if (schema == null)
                    return;

                if (context.Results.Any(x => x.Event.SchemaId == schemaId))
                {
                    // Отпарвляем в архив
                    schema.IsActive = false;
                    schema.LastUpdate = DateTime.UtcNow;
                    context.Schemes.Update(schema);
                }
                else
                {
                    var questions = context.BaseQuestions.Where(x => x.SchemaId == schemaId);
                    if (questions.Any())
                        context.BaseQuestions.RemoveRange(questions);

                    var systemVariable = context.SchemaVariables.Where(x => x.SchemaId == schemaId);
                    if (systemVariable.Any())
                        context.SchemaVariables.RemoveRange(systemVariable);

                    context.Schemes.Remove(schema);

                }
                //Иначе удаляем 

                context.SaveChanges();
            }
        }

        public ResultTemplateView GetResultTemplateDetails(Guid resultTemplateId)
        {
            return _resultService.GetResultTemplateDetails(resultTemplateId);
        }

        public async Task<InterpretationView> UpdateInterpretation(Guid interpretationId, AddOrUpdateInterpretationRequest request)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {

                var interpretation = context.Interpretations.Include(x => x.VariableInInterpretations)
                    .FirstOrDefault(x => x.Guid == interpretationId);

                if (interpretation == null)
                    return null;



                if (interpretation.VariableInInterpretations.Any())
                    context.VariableInInterpretations.RemoveRange(interpretation.VariableInInterpretations);

                if (request.VariableInInterpretationViews.Any())
                    interpretation.VariableInInterpretations =
                        request.VariableInInterpretationViews.Select(x => _mapper.Map<VariableInInterpretation>(x)).ToList();

                interpretation = _mapper.Map(request, interpretation);
                interpretation.LastUpdate = DateTime.UtcNow;

                context.Interpretations.Update(interpretation);
                await context.SaveChangesAsync();

                return _mapper.Map<InterpretationView>(interpretation);
            }
        }

        public async Task RemoveInterpretation(Guid guid)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                var interpretation = await context.Interpretations.Include(x => x.VariableInInterpretations)
                    .FirstOrDefaultAsync(x => x.Guid == guid);

                if (interpretation == null)
                    return;

                if (interpretation.VariableInInterpretations.Any())
                {
                    // Отпарвляем в архив
                    interpretation.IsActive = false;
                    context.Interpretations.Update(interpretation);
                }
                else
                    //Иначе удаляем 
                    context.Interpretations.Remove(interpretation);

                await context.SaveChangesAsync();

            }
        }

        public bool UpdateAnswer(Guid answerId, AddClosedQuestionAnswerRequest request)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                var answer = context.Answers.FirstOrDefault(x => x.Guid == answerId);
                if (answer == null)
                    return false;

                // Проверим индекс 
                if (context.Answers.Any(x =>
                    (x.BaseQuestionGuid == answer.BaseQuestionGuid) && (x.Guid != answer.Guid) &&
                    (x.Index == request.Index)))
                    return false;

                answer.Index = request.Index;
                answer.Text = request.Text;
                answer.VariableValue = request.VariableValue;
                context.SaveChanges();
            }

            return true;
        }

        public bool UpdateQuestion(Guid questionId, UpdateQuestionRequest request)
        {
            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                var question = context.BaseQuestions.FirstOrDefault(x => x.Guid == questionId);
                if (question == null)
                    return false;

                // Проверим индекс 
                if (context.BaseQuestions.Any(x =>
                    (x.SchemaId == question.SchemaId) && (x.Guid != question.Guid) &&
                    (x.Index == request.Index)))
                    return false;

                question.Index = request.Index;
                question.Text = request.Text;
                question.Instruction = request.Instruction;
                question.IsActive = request.IsActive;
                context.SaveChanges();
            }

            return true;
        }
    }
}
