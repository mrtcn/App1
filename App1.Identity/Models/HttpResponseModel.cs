using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App1.Identity.Models
{
    public class HttpResponseModel
    {
        public HttpResponseModel(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Message { get; set; }
        public string Code { get; set; }
    }
}
