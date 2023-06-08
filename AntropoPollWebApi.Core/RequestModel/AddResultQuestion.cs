using System;

namespace AntropoPollWebApi.Core.RequestModel
{
    public class AddResultQuestion
    {
        public Guid BaseQuestionId { get; set; }
        public Guid AnswerId { get; set; }
    }
}
