using System;

namespace AntropoPollWebApi.Core.Models
{
    public class Event : BaseModel
    {
        public string Name { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        public virtual Schema Schema { get; set; }
        public Guid SchemaId { get; set; }

        public virtual User User { get; set; }
        public Guid UserId { get; set; }

        public DateTime? DateTimeBegin { get; set; }
        public DateTime? DateTimeEnd { get; set; }

        public bool IsActive { get; set; }

        public EventActiveType EventActiveType { get; set; }
    }

    /// <summary>
    ///  Auto = 0,
    /// Manual = 1,
    /// </summary>
    public enum EventActiveType
    {
        Auto = 0,
        Manual = 1,
    }
}
