using Crosscutting.Exceptions;
using Domain.Model;
using Domain.Model.Mapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository.Implementation;

namespace RepositoryTests
{
    [TestClass]
    public class ProductRepositoryTests
    {
        private static Mock<IProductMapper> mockMapper;
        private static ProductRepository repository;
        private const int applesStock = 10; //Based on Constants data

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            mockMapper = new Mock<IProductMapper>();
            repository = new ProductRepository(mockMapper.Object);
        }

        [TestMethod]
        public void GivenValidProductName_WhenGettingFromRepository_ShouldRetrieveCorrectDTO()
        {
            //Arrange
            var productName = "apples";

            var expectedMappedProduct = new ProductDTO { Name = productName, Price = 0.2f };

            mockMapper
                .Setup(m => m.Map(It.IsAny<Product>()))
                .Returns(expectedMappedProduct);

            //Act
            var productDTO = repository.GetProduct(productName);

            //Assert
            mockMapper.Verify(m => m.Map(It.IsAny<Product>()), Times.Once);
            Assert.AreEqual(expectedMappedProduct, productDTO);
        }

        [TestMethod]
        public void GivenValidProductName_WhenUpdatingStock_ShouldNotThrowException()
        {
            //Arrange
            var productName = "apples";

            //Act && Assert
            repository.UpdateProductStock(productName, applesStock); //take all the stock
        }

        [TestMethod]
        [ExpectedException(typeof(NotEnoughStockException))]
        public void GivenValidProductNameAndOverCapacity_WhenUpdatingStock_ShouldThrowNotEnoughStockException()
        {
            //Arrange
            var productName = "apples";

            //Act && Assert
            repository.UpdateProductStock(productName, applesStock + 1); //not enough stock
        }
    }
}
