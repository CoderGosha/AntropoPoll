namespace AntropoPollWebApi.Core.Settings
{
    public class DataBaseProviders
    {
        public string Provider { get; set; }

        public ProviderConnections ConnectionStrings
        {
            get;
            set;
        }
    }

    public class ProviderConnections
    {
        public string PostgreSQL { get; set; }
    }
}
