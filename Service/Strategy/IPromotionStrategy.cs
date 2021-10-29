using Service.Helper;

namespace Service.Strategy
{
    public interface IPromotionStrategy
    {
        bool IsActive { get; }

        string Name { get; }

        float DiscountPercentage { get; }

        bool IsApplied(Basket basket);

        float GetDiscountValue(Basket basket);
    }
}
