using System;

namespace RpgGame.UI.Models
{
    public class ValidationException : Exception
    {
        public ValidationException(string message): base(message)
        {
        }
    }
}