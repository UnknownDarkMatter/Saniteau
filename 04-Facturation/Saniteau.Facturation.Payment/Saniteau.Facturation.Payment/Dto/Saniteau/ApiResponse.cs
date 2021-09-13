using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Payment.Dto.Saniteau
{
    public class ApiResponse<TResult>
    {
        public TResult Result { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
    }
}
