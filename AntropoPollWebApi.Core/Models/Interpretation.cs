using System;
using System.Collections.Generic;

namespace AntropoPollWebApi.Core.Models
{
    public class Interpretation : BaseModel
    {
        public string Name { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Строка для разметки в шаблоне
        /// </summary>
        public string Tag { get; set; }

        public virtual Schema Schema { get; set; }
        /// <summary>
        /// Ссылка на схему
        /// </summary>
        public Guid SchemaId { get; set; }

        public bool IsActive { get; set; }

        public virtual IEnumerable<VariableInInterpretation> VariableInInterpretations { get; set; }
    }
}
