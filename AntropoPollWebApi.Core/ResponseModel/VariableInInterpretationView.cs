using System;

namespace AntropoPollWebApi.Core.ResponseModel
{
    public class VariableInInterpretationView
    {
        /// <summary>
        /// Ссылка на переменную. Если переменная дублируется, значит работает правило ИЛИ для диапазона
        /// </summary>
        public Guid SchemaVariableId { get; set; }
        /// <summary>
        /// Минимальное значение
        /// </summary>
        public decimal ValueMin { get; set; }
        /// <summary>
        /// Максимальное значение
        /// </summary>
        public decimal ValueMax { get; set; }
    }
}
