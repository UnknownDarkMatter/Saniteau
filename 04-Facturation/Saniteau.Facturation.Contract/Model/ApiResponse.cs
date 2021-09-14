using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Contract.Model
{
    public class ApiResponse<TResult>
    {
        public TResult Result { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
    }
}
