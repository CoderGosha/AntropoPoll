using AntropoPollWebApi.Core.Models;
using System;

namespace AntropoPollWebApi.Core.ResponseModel
{
    public class QuestionView
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public Guid Guid { get; set; }
        /// <summary>
        /// Номер по порядку для сортировки
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Инструкция к вопросу
        /// </summary>
        public string Instruction { get; set; }
        /// <summary>
        /// Текст вопроса
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// ClosedQuestion = 0,
        /// MatchingQuestion = 1,
        /// OpenQuestion = 2,
        /// QuestionForRanking = 3,
        /// QuestionWithScale = 4
        /// </summary>
        public QuestionType QuestionType { get; set; }

        public ClosedQuestionView ClosedQuestionView { get; set; }

        public Guid? SchemaId { get; set; }
        public Guid? SchemaVariableId { get; set; }
        public bool IsActive { get; set; }
    }
}
