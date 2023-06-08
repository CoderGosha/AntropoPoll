using AntropoPollWebApi.Core.ResponseModel;
using System;
using System.Collections.Generic;

namespace AntropoPollWebApi.Core.RequestModel
{
    public class AddOrUpdateInterpretationRequest
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
        public virtual IEnumerable<VariableInInterpretationView> VariableInInterpretationViews { get; set; }

    }
}
