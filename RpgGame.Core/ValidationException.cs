using System;

namespace RpgGame.Core
{
    public class ValidationException : Exception
    {
        public ValidationException(string message): base(message)
        {
        }
    }
}