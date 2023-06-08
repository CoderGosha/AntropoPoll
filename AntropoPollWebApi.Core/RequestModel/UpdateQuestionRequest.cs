using System;
using AntropoPollWebApi.Core.Models;

namespace AntropoPollWebApi.Core.RequestModel
{
    public class UpdateQuestionRequest
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
        public bool IsActive { get; set; }
    }
}