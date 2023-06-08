using System;

namespace AntropoPollWebApi.Core.ResponseModel
{
    public class ResultQuestionView : BaseView
    {
        public Guid BaseQuestionId { get; set; }
        public string QuestionText { get; set; }
        public Guid AnswerId { get; set; }
        public string AnswerText { get; set; }
    }
}
