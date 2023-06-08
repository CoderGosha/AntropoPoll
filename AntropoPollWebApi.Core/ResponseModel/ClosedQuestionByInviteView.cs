using System.Collections.Generic;

namespace AntropoPollWebApi.Core.ResponseModel
{
    public class ClosedQuestionByInviteView
    {
        /// <summary>
        /// Максимальное количество выборов (по умолчанию 1)
        /// </summary>
        public int MaxCountChoice { get; set; }

        /// <summary>
        /// Минимальное количество выборов (по умолчанию 0)
        /// </summary>
        public int MinCountChoice { get; set; }

        public IList<ClosedQuestionAnswerByInviteView> ClosedQuestionAnswerView { get; set; }
    }
}