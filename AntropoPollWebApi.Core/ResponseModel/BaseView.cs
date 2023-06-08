using System;
using System.ComponentModel.DataAnnotations;

namespace AntropoPollWebApi.Core.ResponseModel
{
    public class BaseView
    {
        [Key]
        public Guid Guid { get; set; }

        /// <summary>
        /// Время последнего обновления
        /// </summary>
        public DateTime LastUpdate { get; set; }
    }
}
