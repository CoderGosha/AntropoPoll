using AntropoPollWebApi.Core.Models;
using System;

namespace AntropoPollWebApi.Core.RequestModel
{
    public class AddQuestionRequest
    {
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
        /// ClosedBaseQuestion = 0,
        /// MatchingQuestion = 1,
        /// OpenQuestion = 2,
        /// QuestionForRanking = 3,
        /// QuestionWithScale = 4
        /// </summary>
        public QuestionType QuestionType { get; set; }

        public AddClosedQuestionRequest AddClosedQuestionRequest { get; set; }

        public Guid? SchemaId { get; set; }

        public Guid? SchemaVariableId { get; set; }

    }
}
