using System;

namespace AntropoPollWebApi.Core.RequestModel
{
    public class AddOrUpdateReportTemplateRequest
    {
        public string Name { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Строка для хранения шаблона
        /// </summary>
        public string Template { get; set; }

        /// <summary>
        /// Ссылка на схему
        /// </summary>
        public Guid SchemaId { get; set; }
    }
}
