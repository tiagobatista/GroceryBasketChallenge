using System.Collections.Generic;
using AutoFixture;
using Crosscutting.Exceptions;
using Crosscutting.Util;
using Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository.Interface;
using Service.Helper;
using Service.Implementation;

namespace ServiceTests
{
    [TestClass]
    public class BasketServiceTests
    {
        private static Mock<IProductRepository> mockProductRepository;
        private static Fixture fixture;
        private static BasketService basketService;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            mockProductRepository = new Mock<IProductRepository>();
            basketService = new BasketService(mockProductRepository.Object);
            fixture = new Fixture();
        }

        [TestMethod]
        public void GivenListOfValidProductsNames_WhenGeneratingBasket_ShouldRetrieveValidBasket()
        {
            //Arrange
            var defaultMockPrice = 2;

            mockProductRepository
                .Setup(p => p.UpdateProductStock(It.IsAny<string>(), It.IsAny<int>()));

            mockProductRepository
                .Setup(p => p.GetProduct(Constants.AppleProductName))
                .Returns(new ProductDTO { Name = Constants.AppleProductName, Price = defaultMockPrice });

            mockProductRepository
                .Setup(p => p.GetProduct(Constants.LoafOfBreadProductName))
                .Returns(new ProductDTO { Name = Constants.LoafOfBreadProductName, Price = defaultMockPrice });

            mockProductRepository
                .Setup(p => p.GetProduct(Constants.SoupProductName))
                .Returns(new ProductDTO { Name = Constants.SoupProductName, Price = defaultMockPrice });

            var listOfProductsNames = new List<string>
            {
                Constants.AppleProductName,
                Constants.AppleProductName,
                Constants.AppleProductName,
                Constants.LoafOfBreadProductName,
                Constants.SoupProductName
            };

            var expectedBasketProducts = new Dictionary<ProductDTO, int>
            {
                { new ProductDTO{ Name = Constants.AppleProductName , Price = defaultMockPrice}, 3 },
                { new ProductDTO{ Name = Constants.LoafOfBreadProductName , Price = defaultMockPrice}, 1 },
                { new ProductDTO{ Name = Constants.SoupProductName , Price = defaultMockPrice}, 1 },
            };

            //Act
            var resultBasket = basketService.GenerateBasket(listOfProductsNames);

            //Assert
            CollectionAssert.AreEqual(expectedBasketProducts, resultBasket.Products);
        }

        [TestMethod]
        [ExpectedException(typeof(ProductNotFoundException))]
        public void GivenListOfValidProductsNames_WhenGeneratingBasket_ShouldThrowProductNotFoundException()
        {
            //Arrange
            var wrongProductName = "Random wrong product";

            mockProductRepository
                .Setup(p => p.GetProduct(wrongProductName))
                .Returns(null as ProductDTO);

            var listOfProductsNames = new List<string>
            {
                wrongProductName
            };

            //Act && Assert
            basketService.GenerateBasket(listOfProductsNames);
        }
    }
}
