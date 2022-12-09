using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
