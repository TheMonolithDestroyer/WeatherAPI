using System.Net;
using AuthAPI.Engine.Exceptions;

namespace AuthAPI.Engine.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger = null)
        {
            _next = next;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var statusCode = null as HttpStatusCode?;
                var message = null as string;

                if (ex is BadRequestException notFoundException)
                {
                    statusCode = notFoundException.StatusCode;
                    message = notFoundException.Message;
                }
                else if (ex is UnauthorizedException unauthException)
                {
                    statusCode = unauthException.StatusCode;
                    message = unauthException.Message;
                }
                else
                {
                    statusCode = HttpStatusCode.InternalServerError;
                    message = ex.Message;
                }

                var exceptionResult = Newtonsoft.Json.JsonConvert.SerializeObject(Result.Failed(message, statusCode.Value));

                if (statusCode == HttpStatusCode.InternalServerError)
                    _logger.LogError(exceptionResult, ex);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)statusCode;
                await context.Response.WriteAsync(exceptionResult);
            }
        }
    }
}
