using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OfficesManager.Domain.MyExceptions
{
    public class ArgumentsForPaginationException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public ArgumentsForPaginationException(string message) : base(message)
        {
            StatusCode = HttpStatusCode.BadRequest;
        }
    }
}
