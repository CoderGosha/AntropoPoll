using System;

namespace AntropoPollWebApi.Core.ResponseModel
{
    public class ClosedQuestionAnswerByInviteView
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
        /// Текст ответа
        /// </summary>
        public string Text { get; set; }
    }
}