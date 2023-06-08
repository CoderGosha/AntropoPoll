using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AntropoPollWebApi.Core.Models
{
    public class ClosedQuestion : BaseQuestion
    {
        /// <summary>
        /// Максимальное количество выборов (по умолчанию 1)
        /// </summary>
        [Range(1, 10)]
        [DefaultValue(1)]
        public int MaxCountChoice { get; set; }

        /// <summary>
        /// Минимальное количество выборов (по умолчанию 0)
        /// </summary>
        [DefaultValue(0)]
        [Range(0, 10)]
        public int MinCountChoice { get; set; }
    }
}
