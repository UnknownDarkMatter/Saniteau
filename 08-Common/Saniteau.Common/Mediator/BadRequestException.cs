using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Common.Mediator
{
    public class BadRequestException : Exception
    {
        public static BadRequestException ActionHasErrors<TAction>(IEnumerable<ActionValidationError> errors)
        {
            string message = $"Command {typeof(TAction).Name} is not valid";

            return new BadRequestException(message, errors);
        }
        public static BadRequestException ActionIsNull(Type actionType)
        {
            string message = $"Command {actionType.Name} is null";

            return new BadRequestException(message);
        }

        public IEnumerable<ActionValidationError> ActionValidationErrors { get; }
        public BadRequestException(string message) : base(message)
        {
            ActionValidationErrors = new ActionValidationError[0];
        }
        public BadRequestException(string message, IEnumerable<ActionValidationError> errors) : base(message)
        {
            ActionValidationErrors = errors ?? new ActionValidationError[0];
        }
    }
}
