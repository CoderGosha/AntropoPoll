using AntropoPollWebApi.Core.Models;
using System;

namespace AntropoPollWebApi.Core.ResponseModel
{
    public class EventView : BaseView
    {
        public string Name { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        public Guid SchemaId { get; set; }

        public Guid UserId { get; set; }

        public DateTime? DateTimeBegin { get; set; }
        public DateTime? DateTimeEnd { get; set; }

        public bool IsActive { get; set; }

        public EventActiveType EventActiveType { get; set; }
    }
}
