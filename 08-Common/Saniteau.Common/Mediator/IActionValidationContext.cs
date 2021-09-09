using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Common.Mediator
{
    public interface IActionValidationContext
    {
        void Add(string message);
        void Add(string message, Exception origin);
    }
}
