using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Common.Mediator
{
    public abstract class BoundedContextBinderBase : IBoundedContextBinder
    {
        protected IServiceCollection ServiceCollection { get; }
        public BoundedContextBinderBase(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection;
        }

        public virtual void BindActionNandlers() { }
    }
}
