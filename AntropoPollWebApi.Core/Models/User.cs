namespace AntropoPollWebApi.Core.Models
{
    public class User : BaseModel
    {
        public bool IsSuperUser { get; set; }
        public bool IsModerator { get; set; }
        public bool IsActive { get; set; }

    }
}
