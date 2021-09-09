using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Saniteau.Common.Mediator
{
    public class ActionValidation<TAction> : IActionValidation<TAction>
    {
        private class CommandValidationContext : IActionValidationContext
        {
            private readonly string _actionPropertyName;
            private readonly ActionValidation<TAction> _notification;

            public CommandValidationContext(string actionPropertyName, ActionValidation<TAction> notification)
            {
                if (actionPropertyName == null) { throw new ArgumentNullException(nameof(actionPropertyName)); }
                if (notification == null) { throw new ArgumentNullException(nameof(notification)); }

                _actionPropertyName = actionPropertyName;
                _notification = notification;
            }

            public void Add(string message)
            {
                _notification.Add(_actionPropertyName, message);
            }

            public void Add(string message, Exception origin)
            {
                _notification.Add(_actionPropertyName, message, origin);
            }
        }

        private static string GetActionPropertyName<TProperty>(Expression<Func<TAction, TProperty>> actionProperty)
        {
            var expression = (MemberExpression)actionProperty.Body;
            MemberInfo member = expression.Member;

            return member.Name;
        }

        private readonly List<ActionValidationError> _errors = new List<ActionValidationError>();

        public void Add<TProperty>(Expression<Func<TAction, TProperty>> actionProperty, string message)
        {
            if (actionProperty == null) { throw new ArgumentNullException(nameof(actionProperty)); }
            if (message == null) { throw new ArgumentNullException(nameof(message)); }

            string actionPropertyName = GetActionPropertyName(actionProperty);

            Add(actionPropertyName, message);
        }

        public void Add<TProperty>(Expression<Func<TAction, TProperty>> actionProperty, string message, Exception origin)
        {
            if (actionProperty == null) { throw new ArgumentNullException(nameof(actionProperty)); }
            if (message == null) { throw new ArgumentNullException(nameof(message)); }
            if (origin == null) { throw new ArgumentNullException(nameof(origin)); }

            string actionPropertyName = GetActionPropertyName(actionProperty);

            Add(actionPropertyName, message, origin);
        }

        public IActionValidationContext CreateContext<TProperty>(Expression<Func<TAction, TProperty>> actionProperty)
        {
            string actionPropertyName = GetActionPropertyName(actionProperty);
            return new CommandValidationContext(actionPropertyName, this);
        }

        public IEnumerator<ActionValidationError> GetEnumerator()
        {
            return _errors.GetEnumerator();
        }

        public IEnumerable<ActionValidationError> GetErrors()
        {
            return _errors;
        }

        public bool HasErrors()
        {
            return _errors.Count > 0;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return _errors.Count switch
            {
                0 => "Command is valid.",
                1 => "Command has one error.",
                _ => $"Command has {_errors.Count} errors."
            };
        }

        private void Add(string actionPropertyName, string message)
        {
            _errors.Add(new ActionValidationError(actionPropertyName, message));
        }
        private void Add(string actionPropertyName, string message, Exception origin)
        {
            _errors.Add(new ActionValidationError(actionPropertyName, message, origin));
        }
    }
}
