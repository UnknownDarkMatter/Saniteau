using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saniteau.Models.Login
{
    public class LoginResponse
    {
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
        public Token Token { get; set; }

        public LoginResponse(bool isError, string errorMessage)
        {
            IsError = isError;
            ErrorMessage = errorMessage;
            Token = null;
        }

        public LoginResponse(bool isError, string errorMessage, Token data)
        {
            IsError = isError;
            ErrorMessage = errorMessage;
            Token = data;
        }
    }
}
