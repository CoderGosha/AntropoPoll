using System;

namespace AntropoPollWebApi.Core.RequestModel
{
    public class UpdateDataReportRequest
    {
        /// <summary>
        /// Данные отчета
        /// </summary>
        public string TemplateData { get; set; }
        public Guid ReportTemplateId { get; set; }
    }
}
