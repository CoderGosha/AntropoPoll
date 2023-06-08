using System;

namespace AntropoPollWebApi.Core.Models
{
    public class SystemVariableReport : BaseModel
    {
        public Guid InviteId { get; set; }
        public virtual Invite Invite { get; set; }

        public virtual SchemaVariable SchemaVariable { get; set; }
        public Guid SchemaVariableId { get; set; }

        public decimal Value { get; set; }
        public decimal MaxValue { get; set; }
        public decimal StanValue { get; set; }
    }
}
