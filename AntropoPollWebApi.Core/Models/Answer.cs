using System;

namespace AntropoPollWebApi.Core.Models
{
    public class Answer : BaseModel
    {
        /// <summary>
        /// Номер по порядку для сортировки
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// Текст ответа
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Значение переменной для отчета
        /// </summary>
        public decimal? VariableValue { get; set; }

        public Guid BaseQuestionGuid { get; set; }
        public virtual BaseQuestion BaseQuestion { get; set; }
    }
}
