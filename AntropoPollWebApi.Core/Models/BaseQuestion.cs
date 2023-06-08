using System;
using System.Collections.Generic;

namespace AntropoPollWebApi.Core.Models
{
    public abstract class BaseQuestion : BaseModel
    {


        /// <summary>
        /// Номер по порядку для сортировки
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// Текст вопроса
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Инструкция к вопросу
        /// </summary>
        public string Instruction { get; set; }

        /// <summary>
        /// ClosedBaseQuestion = 0,
        /// MatchingQuestion = 1,
        /// OpenQuestion = 2,
        /// QuestionForRanking = 3,
        /// QuestionWithScale = 4
        /// </summary>

        public QuestionType QuestionType { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public Guid? SchemaId { get; set; }
        public virtual Schema Schema { get; set; }

        public Guid? SchemaVariableId { get; set; }
        public virtual SchemaVariable SchemaVariable { get; set; }

        public bool IsActive { get; set; }

    }
}
