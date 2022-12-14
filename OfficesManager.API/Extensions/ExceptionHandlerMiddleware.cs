using Newtonsoft.Json;
using OfficesManager.Domain.MyExceptions;
using System.Net;
using Serilog;

namespace OfficesManager.API.Extensions
{
    public class ExceptionHandlerMiddleware
    {
        private RequestDelegate _requestDelegate;
        private IHostEnvironment _env;

        public ExceptionHandlerMiddleware(RequestDelegate requestDelegate, IHostEnvironment env)
        {
            _requestDelegate = requestDelegate;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _requestDelegate.Invoke(httpContext);
            }
            catch (NotFoundException ex)
            {
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.NotFound, () => Log.Information(ex, ex.Message));
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.InternalServerError, () => Log.Error(ex, ex.Message));
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, HttpStatusCode code, Action logAction)
        {
            logAction();

            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)code;
            var allMessageText = GetFullMessage(ex);

            var details = _env.IsDevelopment() ? ex.StackTrace : string.Empty;

            await response.WriteAsync(JsonConvert.SerializeObject(
                new BaseResponseModel(
                    code,
                    allMessageText,
                    string.IsNullOrEmpty(details)
                        ? string.Empty
                        : details)
                ));
        }

        private string GetFullMessage(Exception ex)
        {
            if (ex.InnerException != null)
            {
                return ex.Message + "; " + GetFullMessage(ex.InnerException);
            }

            return ex.Message;
        }
    }
}
