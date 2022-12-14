using System.Net;

namespace OfficesManager.Domain.MyExceptions
{
    public class BaseResponseModel
    {
        public HttpStatusCode StatusCode { get; }

        public string Message { get; }

        public string Details { get; }

        public BaseResponseModel(HttpStatusCode statusCode, string message, string details)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }
    }
}
