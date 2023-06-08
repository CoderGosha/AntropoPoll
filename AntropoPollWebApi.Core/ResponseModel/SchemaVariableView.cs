using System;

namespace AntropoPollWebApi.Core.ResponseModel
{
    public class SchemaVariableView : BaseView
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
    }
}
