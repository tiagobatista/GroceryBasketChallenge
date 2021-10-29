using Crosscutting.Util;
using Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace CrosscuttingTests
{
    [TestClass]
    public class FileReaderTests
    {
        [TestMethod]
        public void GivenValidJson_WhenReadingJson_ShouldTransformToPretendedObject()
        {
            //Arrange
            var file = @"{
                            ""Name"": ""TestProduct"",
                            ""Price"": 0.5,
                            ""Stock"": 10
                        }";

            //Act
            var product = FileReader.GetJsonFileContent<Product>(file);

            //Assert
            Assert.AreEqual("TestProduct", product.Name);
            Assert.AreEqual(0.5f, product.Price);
            Assert.AreEqual(10, product.Stock);
        }

        [TestMethod]
        [ExpectedException(typeof(JsonReaderException))]
        public void GivenInvalidJson_WhenReadingJson_ShouldThrowException()
        {
            //Arrange
            var file = @"{
                            ""Name"": ""TestProduct"",,
                            ""Price"": 0.5,
                            ""Stock"": 10
                        }";

            //Act && Assert
            FileReader.GetJsonFileContent<Product>(file);
        }
    }
}
