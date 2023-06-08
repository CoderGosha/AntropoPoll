using AntropoPollWebApi.Core.Contexts;
using AntropoPollWebApi.Core.Models;
using AntropoPollWebApi.Core.RequestModel;
using AntropoPollWebApi.Core.ResponseModel;
using AntropoPollWebApi.Core.Settings;
using AutoMapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace AntropoPollWebApi.Core.Services.Questions
{
    public class ClosedQuestionService
    {
        private readonly IMapper _mapper;
        private AntropoPollSettings _options;

        public ClosedQuestionService(IMapper mapper, IOptions<AntropoPollSettings> options)
        {
            _mapper = mapper;
            _options = options.Value;
        }

        public QuestionView AddQuestion(AddQuestionRequest addQuestionRequest)
        {
            var question = _mapper.Map<ClosedQuestion>(addQuestionRequest);
            if (addQuestionRequest.AddClosedQuestionRequest == null)
                throw new Exception("AddClosedQuestionRequest is null");

            if (addQuestionRequest.AddClosedQuestionRequest?.AddClosedQuestionAnswerRequest == null)
                throw new Exception("AddClosedQuestionAnswerRequest is null");


            _mapper.Map(addQuestionRequest.AddClosedQuestionRequest, question);
            question.Answers = _mapper.Map<List<Answer>>(addQuestionRequest.AddClosedQuestionRequest.AddClosedQuestionAnswerRequest);

            using (var context = new AntropoPollContext(_options.AntropoPollProviders))
            {
                question.LastUpdate = DateTime.UtcNow;
                context.ClosedQuestion.Add(question);

                context.SaveChanges();
            }

            var questionView = _mapper.Map<QuestionView>(question);
            questionView.ClosedQuestionView = _mapper.Map<ClosedQuestionView>(question);

            return questionView;
        }
    }
}
