using System;

namespace Crosscutting.Exceptions
{
    public class InvalidProductAttributeException: Exception
    {
        public InvalidProductAttributeException(string message): base(message)
        {
        }
    }
}
