using System;

namespace AntropoPollWebApi.Core.ResponseModel
{
    public class SystemVariableReportView
    {
        public SchemaVariableView SchemaVariable { get; set; }
        public Guid SchemaVariableId { get; set; }

        public decimal Value { get; set; }
        public decimal MaxValue { get; set; }
        public decimal StanValue { get; set; }
    }
}
