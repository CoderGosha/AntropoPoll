using System;
using System.Collections.Generic;

namespace AntropoPollWebApi.Core.Models
{
    public class ReportTemplate : BaseModel
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

        public virtual Schema Schema { get; set; }
        /// <summary>
        /// Ссылка на схему
        /// </summary>
        public Guid SchemaId { get; set; }

        public virtual IEnumerable<ResultTemplate> ResultTemplates { get; set; }
    }
}
