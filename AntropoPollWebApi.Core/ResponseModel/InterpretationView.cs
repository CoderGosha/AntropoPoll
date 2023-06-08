using System;
using System.Collections.Generic;

namespace AntropoPollWebApi.Core.ResponseModel
{
    public class InterpretationView : BaseView
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
        /// <summary>
        /// Ссылка на схему
        /// </summary>
        public Guid SchemaId { get; set; }
        public bool IsActive { get; set; }
        public virtual IEnumerable<VariableInInterpretationView> VariableInInterpretationViews { get; set; }

    }
}
