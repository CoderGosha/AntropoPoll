using System.Collections.Generic;
using System.ComponentModel;

namespace AntropoPollWebApi.Core.RequestModel
{
    public class AddClosedQuestionRequest
    {
        /// <summary>
        /// Максимальное количество выборов (по умолчанию 1)
        /// </summary>
        [DefaultValue(1)]
        public int MaxCountСhoice { get; set; }

        /// <summary>
        /// Минимальное количество выборов (по умолчанию 0)
        /// </summary>
        [DefaultValue(0)]
        public int MinCountСhoice { get; set; }

        public List<AddClosedQuestionAnswerRequest> AddClosedQuestionAnswerRequest { get; set; }
    }
}
