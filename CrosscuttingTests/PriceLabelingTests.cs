using Crosscutting.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrosscuttingTests
{
    [TestClass]
    public class PriceLabelingTests
    {
        [TestMethod]
        [DataRow(1.00f, "£1")]
        [DataRow(1.20f, "£1.2")]
        [DataRow(1.20f, "£1.2")]
        [DataRow(0.75f, "75p")]
        public void GivenPriceAsFloat_WhenLabelingPrice_ShouldRetrieveUserFriendlyPrice(float price, string expectedLabel)
        {
            //Arrange & Act
            var resultLabel = PriceLabeling.GetUserFriendlyPrice(price);

            //Assert
            Assert.AreEqual(expectedLabel, resultLabel);
        }
    }
}
