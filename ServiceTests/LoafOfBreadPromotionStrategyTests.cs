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
    public class LoafOfBreadPromotionStrategyTests
    {
        private static LoafOfBreadPromotionStrategy strategy;
        private static Fixture fixture;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            strategy = new LoafOfBreadPromotionStrategy();
            fixture = new Fixture();
        }

        [TestMethod]
        [DataRow(0.2f, 2, 4, 0.2f, true)]
        [DataRow(0.2f, 2, 3, 0.1f, true)]
        [DataRow(0.2f, 1, 2, 0.1f, true)]
        [DataRow(0.2f, 1, 1, 0, false)]
        public void GivenBasketWithBreadAndSoup_WhenGettingDiscountValue_ChecksIfValueIsCorrect(
            float breadPrice,
            int breadQuantity,
            int soupTinQuantity,
            float expectedValue,
            bool isStrategyAppliableExpected)
        {
            //Arrange
            var bread = fixture
                .Build<ProductDTO>()
                .With(p => p.Name, Constants.LoafOfBreadProductName)
                .With(p => p.Price, breadPrice)
                .Create();

            var soup = fixture
                .Build<ProductDTO>()
                .With(p => p.Name, Constants.SoupProductName)
                .Create();

            var basket = new Basket();

            basket.Products.Add(bread, breadQuantity);
            basket.Products.Add(soup, soupTinQuantity);

            //Act
            var isStrategyAppliable = strategy.IsApplied(basket);
            var discountValue = strategy.GetDiscountValue(basket);

            //Assert
            Assert.AreEqual(isStrategyAppliableExpected, isStrategyAppliable);
            Assert.AreEqual(expectedValue, discountValue);
        }

        [TestMethod]
        public void GivenBasketWithApples_WhenCheckingIfStrategyIsApplyiable_DoesNotApplyStrategy()
        {
            //Arrange
            var apples = fixture
                .Build<ProductDTO>()
                .With(p => p.Name, Constants.AppleProductName)
                .Create();

            var basket = new Basket();

            basket.Products.Add(apples, 2);

            //Act
            var isStrategyApplyiable = strategy.IsApplied(basket);

            //Assert
            Assert.IsFalse(isStrategyApplyiable);
        }

        [TestMethod]
        [ExpectedException(typeof(DiscountCalculationException))]
        public void GivenBasketWithApples_WhenGettingDiscountValue_ThrowsDiscountCalculationException()
        {
            //Arrange
            var apples = fixture
                .Build<ProductDTO>()
                .With(p => p.Name, Constants.AppleProductName)
                .Create();

            var basket = new Basket();

            basket.Products.Add(apples, 2);

            //Act && Assert
            strategy.GetDiscountValue(basket);
        }
    }
}
