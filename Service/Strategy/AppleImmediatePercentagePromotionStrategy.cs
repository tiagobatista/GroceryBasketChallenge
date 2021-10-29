using Crosscutting.Exceptions;
using Crosscutting.Util;
using Service.Helper;

namespace Service.Strategy
{
    public class AppleImmediatePercentagePromotionStrategy : IPromotionStrategy
    {
        public bool IsActive { get => Constants.IsAppleImmediatePercentagePromotionActive; }

        public float DiscountPercentage { get => Constants.AppleDiscountPercentage; }

        public string Name => $"Apple {DiscountPercentage} off";

        public float GetDiscountValue(Basket basket)
        {
            var apples = basket.FindProduct(Constants.AppleProductName);

            if(apples is null)
            {
                throw new DiscountCalculationException(Constants.DiscountCalculationExceptionMessage);
            }

            var applesQuantity = basket.GetProductQuantity(Constants.AppleProductName);

            var discountValue =  applesQuantity * (apples.Price * ( DiscountPercentage / 100 ));

            basket.PromotionsApplied.Add($"{Name}: -{PriceLabeling.GetUserFriendlyPrice(discountValue)}");

            return discountValue;
        }

        public bool IsApplied(Basket basket)
        {
            return IsActive && basket.FindProduct(Constants.AppleProductName) != null;
        }
    }
}
