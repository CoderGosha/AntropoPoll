using System;

namespace AntropoPollWebApi.Core.Models
{
    public class UserClaims
    {
        public Guid Guid { get; set; }
        public bool IsSuperUser { get; set; }
        public bool IsModerator { get; set; }
    }
}
