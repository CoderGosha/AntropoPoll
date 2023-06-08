using System;
using System.Collections.Generic;

namespace AntropoPollWebApi.Core.Models
{
    public class SchemaVariable : BaseModel
    {
        /// <summary>
        /// Имя переменной
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        public Guid? SchemaId { get; set; }
        public virtual Schema Schema { get; set; }

        public virtual IList<BaseQuestion> BaseQuestions { get; set; }
    }
}
