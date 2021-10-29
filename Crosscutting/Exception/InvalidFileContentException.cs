using System;

namespace Crosscutting.Exceptions
{
    public class InvalidFileContentExcetion: Exception
    {
        public InvalidFileContentExcetion(string message) : base(message)
        {
        }
    }
}
