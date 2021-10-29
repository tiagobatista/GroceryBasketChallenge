using System.Collections.Generic;
using AutoFixture;
using Crosscutting.Util;
using Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service.Helper;
using Service.Implementation;
using Service.Strategy;

namespace ServiceTests
{
    [TestClass]
    public class CheckoutServiceTests
    {
        private static Fixture fixture;
        private static IPromotionStrategy[] strategies;
        private static Mock<IPromotionStrategy> mockApplePromotionStrategy;
        private static Mock<IPromotionStrategy> mockBreadPromotionStrategy;
        private static CheckoutService service;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            fixture = new Fixture();

            mockApplePromotionStrategy = new Mock<IPromotionStrategy>();
            mockBreadPromotionStrategy = new Mock<IPromotionStrategy>();

            strategies = new IPromotionStrategy[] { mockApplePromotionStrategy.Object, mockBreadPromotionStrategy.Object };


            service = new CheckoutService(strategies);
        }

        [TestMethod]
        public void GivenValidBasketWithoutOffers_WhenCheckingOut_ShouldRetrieveCorrectReceipt()
        {
            //Arrange
            var products = new Dictionary<ProductDTO, int>
            {
                { 
                    fixture
                .Build<ProductDTO>()
                .With(p => p.Name, Constants.MilkProductName)
                .Create(),
                2 
                }
            };

            mockApplePromotionStrategy
                .SetupGet(s => s.IsActive)
                .Returns(false);

            mockBreadPromotionStrategy
                .SetupGet(s => s.IsActive)
                .Returns(false);

            var basket = fixture
                .Build<Basket>()
                .With(b => b.Products, products)
                .With(b => b.PromotionsApplied, new List<string>())
                .Create();

            //Act
            var receipt = service.ProcessBasket(basket);

            //Assert
            Assert.IsTrue(receipt.Contains(Constants.NoOffersAvailableMessage));
        }

        [TestMethod]
        public void GivenValidBasketWithApplesOffers_WhenCheckingOut_ShouldRetrieveCorrectReceipt()
        {
            //Arrange
            var mockPromotionName = "apple promotion";

            var products = new Dictionary<ProductDTO, int>
            {
                { 
                    fixture
                .Build<ProductDTO>()
                .With(p => p.Name, Constants.AppleProductName)
                .Create(),
                2 
                }
            };
            
            mockApplePromotionStrategy
                 .SetupGet(s => s.IsActive)
                 .Returns(true);
            
            mockApplePromotionStrategy
                 .SetupGet(s => s.Name)
                 .Returns(mockPromotionName);

            mockBreadPromotionStrategy
                .SetupGet(s => s.IsActive)
                .Returns(false);

            var basket = fixture
                .Build<Basket>()
                .With(b => b.Products, products)
                .With(b => b.PromotionsApplied, new List<string>())
                .Create();

            //Act
            var receipt = service.ProcessBasket(basket);

            //Assert
            Assert.IsTrue(receipt.Contains(mockPromotionName));
        }
    }
}
