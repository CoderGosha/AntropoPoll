namespace AntropoPollWebApi.Core.ResponseModel
{
    public class SchemaView : BaseView
    {
        public string Name { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
        public int StanValue { get; set; }
        public bool IsActive { get; set; }
    }
}
