namespace Crosscutting.Util
{
    public static class PriceLabeling
    {
        /// <summary>
        /// Converts value to user friendly value in GBP
        /// </summary>
        /// <param name="value">Value of currency</param>
        /// <returns>User friendly currency designation</returns>
        public static string GetUserFriendlyPrice(float value)
        {
            return value < 1 ? $"{value % 1 * 100}p" : $"£{value}";
        }
    }
}
