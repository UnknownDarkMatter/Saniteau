using System;

namespace Saniteau.Common.Mediator
{
    public class ActionValidationError
    {
        public string FieldName { get; }
        public string Message { get; }
        public Exception Origin { get; }
        public ActionValidationError(string fieldName, string message)
        {
            if (fieldName == null) { throw new ArgumentNullException(nameof(fieldName)); }
            if (message == null) { throw new ArgumentNullException(nameof(message)); }


            FieldName = fieldName;
            Message = message;
            Origin = null;
        }
        public ActionValidationError(string fieldName, string message, Exception origin) : this (fieldName, message)
        {
            Origin = origin;
        }
        public override string ToString()
        {
            return FieldName == null ? Message : $"{FieldName} : {Message}";
        }
    }
}
