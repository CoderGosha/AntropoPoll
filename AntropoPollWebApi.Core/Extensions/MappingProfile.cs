using AntropoPollWebApi.Core.Models;
using AntropoPollWebApi.Core.RequestModel;
using AntropoPollWebApi.Core.ResponseModel;
using AutoMapper;
using System;
using System.Text.Json;

namespace AntropoPollWebApi.Core.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddQuestionRequest, ClosedQuestion>();
            CreateMap<AddClosedQuestionRequest, ClosedQuestion>();
            //    .ForMember(dest => dest.Answers, o => o.MapFrom((src, dest) => dest.Answers = src.AddClosedQuestionAnswerRequest));


            CreateMap<ClosedQuestion, QuestionView>();
            CreateMap<ClosedQuestion, ClosedQuestionView>();

            CreateMap<AddClosedQuestionAnswerRequest, Answer>();
            CreateMap<Answer, ClosedQuestionAnswerView>();

            CreateMap<ClosedQuestion, QuestionInviteView>();
            CreateMap<ClosedQuestion, ClosedQuestionByInviteView>();
            CreateMap<Answer, ClosedQuestionAnswerByInviteView>();

            CreateMap<Schema, SchemaView>();
            CreateMap<AddOrUpdateSchemaRequest, Schema>();

            CreateMap<Result, ResultView>()
                .ForMember(dest => dest.FormAnalytics, o =>
                    o.MapFrom((src, dest) => dest.FormAnalytics = src.FormAnalytics.RootElement));

            CreateMap<Result, ResultDetailsView>()
                .ForMember(dest => dest.FormAnalytics, o =>
                    o.MapFrom((src, dest) => dest.FormAnalytics = src.FormAnalytics.RootElement));

            CreateMap<AddResultRequest, Result>()
                .ForMember(dest => dest.FormAnalytics, o =>
                    o.MapFrom((src, dest) => dest.FormAnalytics = JsonDocument.Parse(src.FormAnalytics.ToString())));

            CreateMap<ResultQuestion, ResultQuestionView>()
                .ForMember(dest => dest.QuestionText, o =>
                    o.MapFrom(src => src.BaseQuestion != null
                        ? src.BaseQuestion.Text
                        : null))

                .ForMember(dest => dest.AnswerText, o =>
                    o.MapFrom(src => src.Answer != null
                        ? src.Answer.Text
                        : null));


            CreateMap<AddResultQuestion, ResultQuestion>();

            CreateMap<SchemaVariable, SchemaVariableView>();
            CreateMap<AddSchemaVariableRequest, SchemaVariable>();

            CreateMap<User, UserView>();
            CreateMap<AddOrUpdateUserRequest, User>();

            CreateMap<Invite, InviteView>();

            CreateMap<SystemVariableReport, SystemVariableReportView>();


            CreateMap<Event, EventView>();
            CreateMap<AddEventRequest, Event>();

            CreateMap<Interpretation, InterpretationView>();

            CreateMap<AddOrUpdateInterpretationRequest, Interpretation>();
            CreateMap<VariableInInterpretation, VariableInInterpretationView>();
            CreateMap<VariableInInterpretationView, VariableInInterpretation>()
                .ForMember(dest => dest.LastUpdate, o =>
                    o.MapFrom((src, dest) => dest.LastUpdate = DateTime.UtcNow));

            CreateMap<ReportTemplate, ReportTemplateView>();
            CreateMap<AddOrUpdateReportTemplateRequest, ReportTemplate>();

            CreateMap<ResultTemplate, ResultTemplateView>();
        }
    }
}
