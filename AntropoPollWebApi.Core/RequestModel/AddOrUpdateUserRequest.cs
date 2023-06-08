namespace AntropoPollWebApi.Core.RequestModel
{
    public class AddOrUpdateUserRequest
    {
        public bool IsSuperUser { get; set; }
        public bool IsModerator { get; set; }
        public bool IsActive { get; set; }
    }
}
