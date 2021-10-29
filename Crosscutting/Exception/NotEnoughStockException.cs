using System;

namespace Crosscutting.Exceptions
{
    public class NotEnoughStockException: Exception
    {
        public NotEnoughStockException(string message) : base(message)
        {

        }
    }
}
