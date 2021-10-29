using System;

namespace Crosscutting.Exceptions
{
    public class DiscountCalculationException: Exception
    {
        public DiscountCalculationException(string message): base(message)
        {

        }
    }
}
