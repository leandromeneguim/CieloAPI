using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CieloUmbler.MVC.Models
{
    public class ErrorApi
    {
        public string StatusCode { get; private set; }
        public string Message { get; private set; }

        public ErrorApi(string statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
