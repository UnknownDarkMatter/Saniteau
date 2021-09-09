using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Common.Mediator
{
    public class BoundedContextBinders
    {
        private readonly List<IBoundedContextBinder> _binders = new List<IBoundedContextBinder>();

        public void BindAll()
        {
            foreach(IBoundedContextBinder binder in _binders)
            {
                binder.BindActionNandlers();
            }
        }

        public void Register(IBoundedContextBinder binder)
        {
            if(binder == null) { throw new ArgumentNullException(nameof(binder));  }
            if(_binders.Contains(binder)) { throw new ArgumentException($"Binder {binder.GetType().FullName} is already registrered."); }

            _binders.Add(binder);
        }
    }
}
