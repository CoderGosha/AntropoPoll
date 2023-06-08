using System;

namespace AntropoPollWebApi.Core.Models
{
    public class ResultQuestion : BaseModel
    {
        public Guid BaseQuestionId { get; set; }
        public BaseQuestion BaseQuestion { get; set; }
        public Guid AnswerId { get; set; }
        public Answer Answer { get; set; }
        public Guid ResultId { get; set; }
        public Result Result { get; set; }

    }
}
