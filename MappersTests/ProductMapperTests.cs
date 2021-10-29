using Crosscutting.Exceptions;
using Domain.Model;
using Domain.Model.Mapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MappersTests
{
    [TestClass]
    public class ProductMapperTests
    {
        private static ProductMapper mapper;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            mapper = new ProductMapper();
        }

        [TestMethod]
        public void GivenNullProduct_WhenMappingToDTO_ShouldMapNull()
        {
            //Arrange && Act
            var productDTOResult = mapper.Map(null);

            //Assert
            Assert.IsNull(productDTOResult);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [ExpectedException(typeof(InvalidProductAttributeException))]
        public void GivenProductWithInvalidName_WhenMappingToDTO_ShouldThrowInvalidProductAttributeException(string invalidName)
        {
            //Arrange
            var product = new Product { Name = invalidName, Price = 0.5f };

            //Act && Assert
            mapper.Map(product);
        }

        [TestMethod]
        [DataRow(0f)]
        [DataRow(-1f)]
        [ExpectedException(typeof(InvalidProductAttributeException))]
        public void GivenInvalidPrice_WhenMappingToDTO_ShouldMapCorrectly(float price)
        {
            //Arrange
            var product = new Product { Name = "test", Price = price };

            //Act && Assert
            var productDTOResult = mapper.Map(product);
        }

        [TestMethod]
        public void GivenValidProduct_WhenMappingToDTO_ShouldMapCorrectly()
        {
            //Arrange
            var product = new Product { Name = "test", Price = 0.5f };

            var expectedProductDTO = new ProductDTO { Name = "test", Price = 0.5f };

            //Act
            var productDTOResult = mapper.Map(product);

            //Assert
            Assert.IsNotNull(productDTOResult);
            Assert.AreEqual(expectedProductDTO, productDTOResult);
        }
    }
}
