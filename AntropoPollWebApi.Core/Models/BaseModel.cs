using System;
using System.ComponentModel.DataAnnotations;

namespace AntropoPollWebApi.Core.Models
{
    public class BaseModel
    {
        [Key]
        public Guid Guid { get; set; }

        /// <summary>
        /// Время последнего обновления
        /// </summary>
        public DateTime LastUpdate { get; set; }


    }
}
