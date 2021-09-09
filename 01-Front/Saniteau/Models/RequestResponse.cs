using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saniteau.Models
{
    public class RequestResponse
    {
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }

        public RequestResponse(bool isError, string errorMessage)
        {
            IsError = isError;
            ErrorMessage = errorMessage;
        }
    }
}
