using System.Linq;
using Crosscutting.Util;
using Service.Helper;
using Service.Interface;
using Service.Strategy;

namespace Service.Implementation
{
    public class CheckoutService: ICheckoutService
    {
        private IPromotionStrategy[] promotionStrategies;

        public CheckoutService(IPromotionStrategy[] promotionStrategies)
        {
            this.promotionStrategies = promotionStrategies;
        }

        public string ProcessBasket(Basket basket)
        {
            var subTotal = basket.Products.Keys.Sum(p => p.Price * basket.Products[p]);

            var total = subTotal;

            foreach (var promotionStrategy in promotionStrategies)
            {
                if (promotionStrategy.IsApplied(basket))
                {
                    total -= promotionStrategy.GetDiscountValue(basket);
                }
            }

            var subTotalMessage = $"Subtotal: {PriceLabeling.GetUserFriendlyPrice(subTotal)}\n\n";
            var promotionsApplied = "";

            if (!basket.PromotionsApplied.Any())
            {
                promotionsApplied = $"{Constants.NoOffersAvailableMessage}\n\n";
            }
            else
            {
                basket.PromotionsApplied.ForEach(p => promotionsApplied += $"{p}\n\n");
            }

            return $"Subtotal: {PriceLabeling.GetUserFriendlyPrice(subTotal)}\n\n"
                 + $"{promotionsApplied}"
                 + $"Total: {PriceLabeling.GetUserFriendlyPrice(total)}";
                   
        }
    }
}
