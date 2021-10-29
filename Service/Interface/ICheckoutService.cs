using Service.Helper;

namespace Service.Interface
{
    /// <summary>
    /// Has the responsability of processing the checkout payment value based on a basket and active promotions
    /// </summary>
    public interface ICheckoutService
    {
        string ProcessBasket(Basket basket);
    }
}
