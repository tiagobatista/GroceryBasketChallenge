using AutoFixture;
using Crosscutting.Exceptions;
using Crosscutting.Util;
using Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Helper;
using Service.Strategy;

namespace ServiceTests
{
    [TestClass]
    public class AppleImmediatePercentagePromotionStrategyTests
    {
        private static AppleImmediatePercentagePromotionStrategy strategy;
        private static Fixture fixture;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            strategy = new AppleImmediatePercentagePromotionStrategy();
            fixture = new Fixture();
        }

        [TestMethod]
        [DataRow(1, 2, 0.2f)]
        [DataRow(2, 2, 0.4f)]
        [DataRow(3, 1, 0.3f)]
        public void GivenBasketWithApples_WhenGettingDiscountValue_ChecksIfValueIsCorrect(
            float applesPrice,
            int applesQuantity,
            float expectedValue)
        {
            //Arrange
            var apples = fixture
                .Build<ProductDTO>()
                .With(p => p.Name, Constants.AppleProductName)
                .With(p => p.Price, applesPrice)
                .Create();

            var basket = new Basket();

            basket.Products.Add(apples, applesQuantity);

            //Act
            var isStrategyAppliable = strategy.IsApplied(basket);
            var discountValue = strategy.GetDiscountValue(basket);

            //Assert
            Assert.IsTrue(isStrategyAppliable);
            Assert.AreEqual(expectedValue, discountValue);
        }

        [TestMethod]
        public void GivenBasketWithoutApples_WhenCheckingIfStrategyIsApplyiable_DoesNotApplyStrategy()
        {
            //Arrange
            var bread = fixture
                .Build<ProductDTO>()
                .With(p => p.Name, Constants.LoafOfBreadProductName)
                .Create();

            var basket = new Basket();

            basket.Products.Add(bread, 2);

            //Act
            var isStrategyApplyiable = strategy.IsApplied(basket);

            //Assert
            Assert.IsFalse(isStrategyApplyiable);
        }

        [TestMethod]
        [ExpectedException(typeof(DiscountCalculationException))]
        public void GivenBasketWithBread_WhenGettingDiscountValue_ThrowsDiscountCalculationException()
        {
            //Arrange
            var bread = fixture
                .Build<ProductDTO>()
                .With(p => p.Name, Constants.LoafOfBreadProductName)
                .Create();

            var basket = new Basket();

            basket.Products.Add(bread, 2);

            //Act && Assert
            strategy.GetDiscountValue(basket);
        }
    }
}
