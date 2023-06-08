namespace AntropoPollWebApi.Core.RequestModel
{
    public class AddOrUpdateSchemaRequest
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
