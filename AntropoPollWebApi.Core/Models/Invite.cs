using System;

namespace AntropoPollWebApi.Core.Models
{
    public class Invite : BaseModel
    {
        public virtual Event Event { get; set; }
        public Guid EventId { get; set; }
    }
}
