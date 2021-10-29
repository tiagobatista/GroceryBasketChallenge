using System.Collections.Generic;
using Service.Helper;

namespace Service.Interface
{
    /// <summary>
    /// Has the responsability of processing the basket
    /// </summary>
    public interface IBasketService
    {
        /// <summary>
        /// Generates a Basket object with all its information based in an input
        /// </summary>
        /// <param name="input">List of products names</param>
        /// <returns>A Basket object</returns>
        Basket GenerateBasket(List<string> input);
    }
}
