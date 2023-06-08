using System;

namespace AntropoPollWebApi.Core.Models
{
    public class ResultInterpretation : BaseModel
    {
        public Guid InterpretationId { get; set; }
        public virtual Interpretation Interpretation { get; set; }
        public Guid ResultId { get; set; }
        public virtual Result Result { get; set; }
    }
}
