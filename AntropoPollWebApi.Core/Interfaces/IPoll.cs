using AntropoPollWebApi.Core.RequestModel;
using AntropoPollWebApi.Core.ResponseModel;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AntropoPollWebApi.Core.Models;

namespace AntropoPollWebApi.Core.Interfaces
{
    public interface IPoll
    {
        IEnumerable<QuestionView> GetQuestionList(Guid? schemeId, bool includeArchive);
        QuestionView AddQuestion(AddQuestionRequest addQuestionRequest);
        SchemaView AddSchema(AddOrUpdateSchemaRequest request);
        IEnumerable<SchemaView> GetSchemeList(bool? includeArchive);
        SchemaView UpdateSchema(Guid schemaId, AddOrUpdateSchemaRequest request);
        ResultView AddResult(AddResultRequest request);
        IEnumerable<ResultView> GetResultList(Guid? eventId);
        ResultDetailsView GetResultDetails(Guid resultId);
        IEnumerable<SchemaVariableView> GetSchemaVariableList(Guid? schemeId);
        SchemaVariableView AddSchemaVariableRequest(AddSchemaVariableRequest request);
        List<InviteView> GetInviteList(Guid eventId);
        List<InviteView> AddInvites(AddInviteClientsRequest request);
        void ReEvalResult(Guid resultId);
        SchemaView GetSchemaDetails(Guid id);
        EventView AddEvent(AddEventRequest request);
        EventView GetEventDetails(Guid id);
        IEnumerable<EventView> GetEventList(Guid? userId);
        IEnumerable<QuestionInviteView> GetQuestionListByInvite(Guid inviteId);
        Task<int> RemoveQuestion(Guid guid);
        Task<IEnumerable<InterpretationView>> GetInterpretationList(Guid? schemeId, bool? includeArchive);
        Task<InterpretationView> AddInterpretation(AddOrUpdateInterpretationRequest request);
        Task<IEnumerable<ReportTemplateView>> GetReportTemplateList(Guid? schemeId);
        Task<ReportTemplateView> AddReportTemplate(AddOrUpdateReportTemplateRequest request);
        ResultDetailsView UpdateDataResultReport(Guid resultId, UpdateDataReportRequest templateData);
        ResultDetailsView CompletedResultReport(Guid resultId);
        void RemoveSchema(Guid schemaId);
        ResultTemplateView GetResultTemplateDetails(Guid resultTemplateId);
        Task<InterpretationView> UpdateInterpretation(Guid interpretationId, AddOrUpdateInterpretationRequest request);
        Task RemoveInterpretation(Guid guid);
        bool UpdateAnswer(Guid answerId, AddClosedQuestionAnswerRequest request);
        bool UpdateQuestion(Guid questionId, UpdateQuestionRequest request);
    }
}
