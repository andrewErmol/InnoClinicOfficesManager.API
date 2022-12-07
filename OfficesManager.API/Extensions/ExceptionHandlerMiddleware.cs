using Newtonsoft.Json;
using OfficesManager.Domain.MyExceptions;
using System.Net;

namespace OfficesManager.API.Extensions
{
    public class ExceptionHandlerMiddleware
    {
        public RequestDelegate requestDelegate;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionHandlerMiddleware> logger)
        {
            this.requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await requestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private Task HandleException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            switch (ex)
            {
                case NotFoundException:                    
                    _logger.LogError(ex.ToString());
                    context.Response.StatusCode = (int)((NotFoundException)ex).StatusCode;
                    
                    return context.Response.WriteAsync(JsonConvert.SerializeObject(new { Message = ex.Message, Code = (int)((NotFoundException)ex).StatusCode }));
                    break;

                case ArgumentsForPaginationException:
                    _logger.LogError(ex.ToString());
                    context.Response.StatusCode = (int)((ArgumentsForPaginationException)ex).StatusCode;

                    return context.Response.WriteAsync(JsonConvert.SerializeObject(new { Message = ex.Message, Code = (int)((ArgumentsForPaginationException)ex).StatusCode }));
                    break;

                default:
                    _logger.LogError(ex.ToString());
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    return context.Response.WriteAsync(JsonConvert.SerializeObject(new { Message = ex.Message, Code = (int)HttpStatusCode.InternalServerError }));
                    break;
            }
        }
    }
}
