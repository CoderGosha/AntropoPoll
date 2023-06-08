using System;
using System.Collections.Generic;
using System.Text.Json;

namespace AntropoPollWebApi.Core.Models
{
    public class Result : BaseModel
    {
        public ResultStatus Status { get; set; }
        public Guid InviteId { get; set; }
        public virtual Invite Invite { get; set; }

        // [InverseProperty("ResultQuestion")]
        public virtual IList<ResultQuestion> ResultQuestions { get; set; }
        public Guid EventId { get; set; }
        public Event Event { get; set; }
        public virtual IList<ResultInterpretation> ResultInterpretations { get; set; }
        public JsonDocument FormAnalytics { get; set; }
        public virtual IEnumerable<ResultTemplate> ResultTemplates { get; set; }
    }
}
