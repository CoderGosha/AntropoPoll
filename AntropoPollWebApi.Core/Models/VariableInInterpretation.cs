using System;

namespace AntropoPollWebApi.Core.Models
{
    public class VariableInInterpretation : BaseModel
    {
        public virtual Interpretation Interpretation { get; set; }
        public Guid InterpretationId { get; set; }

        public virtual SchemaVariable SchemaVariable { get; set; }
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
