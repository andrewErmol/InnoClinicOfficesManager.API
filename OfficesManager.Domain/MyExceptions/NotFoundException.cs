using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OfficesManager.Domain.MyExceptions
{
    public class NotFoundException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public NotFoundException(string message) : base(message)
        {
            StatusCode = HttpStatusCode.NotFound;
        }
    }
}
