using System;

namespace Crosscutting.Exceptions
{
    public class InputException : Exception
    {
        public InputException(string message) : base(message)
        {
        }
    }
}
