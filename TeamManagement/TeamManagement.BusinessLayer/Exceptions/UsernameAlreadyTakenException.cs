using System;

namespace TeamManagement.BusinessLayer.Exceptions
{
    public class UsernameAlreadyTakenException : Exception
    {
        private const String MESSAGE = "Username was already taken.";

        public UsernameAlreadyTakenException()
            : base(MESSAGE) { }

        public UsernameAlreadyTakenException(String message)
            : base(message) { }
    }
}
