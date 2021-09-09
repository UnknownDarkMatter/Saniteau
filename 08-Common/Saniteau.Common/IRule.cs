using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Common
{
    public abstract class IRule
    {
        public abstract bool IsSatisfied();
        public abstract void ExecuteRule();

    }
}
