using System;

namespace Superheroes.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string memberName, string errorMessage) : this(errorMessage)
        {
            MemberName = memberName;
        }

        public ValidationException(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string MemberName { get; private set; } = "ErrorMessage";
        public string ErrorMessage { get; private set; }
    }
}