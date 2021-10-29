using Crosscutting.Exceptions;
using Crosscutting.Util;
using Service.Helper;

namespace Service.Strategy
{
    public class LoafOfBreadPromotionStrategy : IPromotionStrategy
    {
        private int NumberOfSoupsToGetLoafOfBreadDiscount = Constants.NumberOfSoupsToGetLoafOfBreadDiscount;

        public bool IsActive { get => Constants.IsLoafOfBreadPromotionActive; }

        public float DiscountPercentage { get => Constants.LoafOfBreadDiscountPercentage; }

        public string Name { get => $"Loaf Of Bread {DiscountPercentage}% off"; }

        public float GetDiscountValue(Basket basket)
        {
            var bread = basket.FindProduct(Constants.LoafOfBreadProductName);

            if (bread is null)
            {
                throw new DiscountCalculationException(Constants.DiscountCalculationExceptionMessage);
            }

            var soupTinQuantity = basket.GetProductQuantity(Constants.SoupProductName);

            var timesToApplyHalfPriceDiscount = soupTinQuantity / NumberOfSoupsToGetLoafOfBreadDiscount;

            var discountValue =  (bread.Price * (DiscountPercentage / 100)) * timesToApplyHalfPriceDiscount;

            basket.PromotionsApplied.Add($"{Name}: -{PriceLabeling.GetUserFriendlyPrice(discountValue)}");

            return discountValue;
        }

        public bool IsApplied(Basket basket)
        {
            var soupTinQuantity = basket.GetProductQuantity(Constants.SoupProductName);

            var breadQuantity = basket.GetProductQuantity(Constants.LoafOfBreadProductName);

            return IsActive && soupTinQuantity >= 2 && breadQuantity > 0;
        }
    }
}
