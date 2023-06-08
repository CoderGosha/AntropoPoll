using System;

namespace AntropoPollWebApi.Core.RequestModel
{
    public class AddSchemaVariableRequest
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
