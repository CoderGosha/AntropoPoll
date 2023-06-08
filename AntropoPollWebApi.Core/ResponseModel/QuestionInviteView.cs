using System;
using AntropoPollWebApi.Core.Models;

namespace AntropoPollWebApi.Core.ResponseModel
{
    public class QuestionInviteView
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

        public ClosedQuestionByInviteView ClosedQuestionView { get; set; }

        public Guid? SchemaId { get; set; }
        public bool IsActive { get; set; }
    }
}