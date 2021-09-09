using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Saniteau.Common.Mediator
{
    public interface IActionValidation<TAction> : IEnumerable<ActionValidationError>
    {
        void Add<TProperty>(Expression<Func<TAction, TProperty>> actionProperty, string message);
        void Add<TProperty>(Expression<Func<TAction, TProperty>> actionProperty, string message, Exception origin);

        IActionValidationContext CreateContext<TProperty>(Expression<Func<TAction, TProperty>> actionProperty);
        IEnumerable<ActionValidationError> GetErrors();
        bool HasErrors();
    }
}
