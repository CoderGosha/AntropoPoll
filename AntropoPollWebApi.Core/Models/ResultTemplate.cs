using System;
using System.Collections.Generic;
using System.Text;

namespace AntropoPollWebApi.Core.Models
{
    public class ResultTemplate : BaseModel 
    {
        public virtual Result Result { get; set; }
        public Guid ResultId { get; set; }
        public virtual ReportTemplate ReportTemplate { get; set; }
        public Guid ReportTemplateId { get; set; }
        public string TemplateData { get; set; }
    }
}
