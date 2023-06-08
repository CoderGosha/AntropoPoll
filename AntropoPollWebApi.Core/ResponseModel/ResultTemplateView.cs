using System;
using System.Collections.Generic;
using System.Text;

namespace AntropoPollWebApi.Core.ResponseModel
{
    public class ResultTemplateView : BaseView
    {
        public Guid ResultId { get; set; }
        public Guid ReportTemplateId { get; set; }
        public string TemplateData { get; set; }
    }
}
