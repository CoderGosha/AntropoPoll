namespace AntropoPollWebApi.Core.ResponseModel
{
    public class UserView : BaseView
    {
        public bool IsSuperUser { get; set; }
        public bool IsModerator { get; set; }
        public bool IsActive { get; set; }
        public string AccessToken { get; set; }
    }
}
