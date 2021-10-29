namespace Crosscutting.Util
{
    public static partial class Constants
    {
        //Exception messages

        public static string EmptyInputExceptionMessage = "Input is empty";

        public static string InvalidInputExceptionMessage = "Input does not respect required rules";

        public static string ProductNotFoundExceptionMessage = "Product was not found in our stock";

        public static string InvalidFileContentExceptionMessage = "File Content is invalid";

        public static string InvalidProductAttributeExceptionMessage = "Invalid Product attributes";

        public static string NotEnoughStockExceptionMessage = "Not enough stock for required product(s)";

        public static string DiscountCalculationExceptionMessage = "Discount calculation error. First check if strategy is applyiable in your code";
    }

    public static partial class Constants
    {
        //Products Names

        public static string AppleProductName = "Apples";

        public static string SoupProductName = "Soup";

        public static string LoafOfBreadProductName = "Bread";

        public static string MilkProductName = "Milk";
    }

    public static partial class Constants
    {
        //File

        public static string DataFile = @"
        [
            {
              ""Name"": ""soup"",
              ""Stock"": 10,
              ""Price"": 0.65
            },
            {
              ""Name"": ""bread"",
              ""Stock"": 10,
              ""Price"": 0.80
            },
            {
              ""Name"": ""milk"",
              ""Stock"": 10,
              ""Price"": 1.3
            },
            {
              ""Name"": ""apples"",
              ""Stock"": 10,
              ""Price"": 1
            }
        ]";
    }

    public static partial class Constants
    {
        //Input 

        public static string InputBeggining = "PriceBasket";
    }

    public static partial class Constants
    {
        //Output 

        public static string NoOffersAvailableMessage = "(no offers available)";
    }

    public static partial class Constants
    {
        //Discounts settings

        //Loaf of bread
        public static int NumberOfSoupsToGetLoafOfBreadDiscount = 2;
        public static int LoafOfBreadDiscountPercentage = 50;
        public static bool IsLoafOfBreadPromotionActive = true;

        //Apples
        public static int AppleDiscountPercentage = 10;
        public static bool IsAppleImmediatePercentagePromotionActive = true;
    }
}
