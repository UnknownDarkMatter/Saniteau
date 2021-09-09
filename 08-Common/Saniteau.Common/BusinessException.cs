using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Common
{
    public class BusinessException : Exception
    {
        public BusinessException(string errorMessage) : base(errorMessage)
        {

        }
    }
}
