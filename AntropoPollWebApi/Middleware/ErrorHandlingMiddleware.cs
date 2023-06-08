using AntropoPollWebApi.RequestModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NLog;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AntropoPollWebApi.Middleware
{
    /// <summary>
    /// Middleware для перехватки исключений
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly ILogger _logger = LogManager.GetLogger("ExceptionLogger");
        private readonly RequestDelegate _next;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="next"></param>
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Перехватка исключений и запись в лог
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            var exceptionMessage = new HttpResponseException()
            {
                Message = "An error has occurred.",
                ExceptionMessage = exception.Message,
                ExceptionType = exception.GetType().ToString(),
                StackTrace = exception.StackTrace
            };
            var result = JsonConvert.SerializeObject(exceptionMessage);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
