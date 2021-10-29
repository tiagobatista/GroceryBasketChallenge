using System;

namespace Crosscutting.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string message) : base(message)
        {
        }
    }
}
