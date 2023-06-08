namespace AntropoPollWebApi.RequestModels
{
    public class HttpResponseException
    {
        public string ExceptionMessage { get; set; }
        public string ExceptionType { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }

    }
}
