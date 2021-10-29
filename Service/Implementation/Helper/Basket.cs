using System.Collections.Generic;
using System.Linq;
using Domain.Model;

namespace Service.Helper
{
    /// <summary>
    /// Represents a group of products and their quantity respectively and the promotions applied to the basket
    /// </summary>
    public class Basket
    {
        public Basket()
        {
            Products = new Dictionary<ProductDTO, int>();
            PromotionsApplied = new List<string>();
        }

        public Dictionary<ProductDTO, int> Products { get; set; }

        public List<string> PromotionsApplied { get; set; }

        public ProductDTO FindProduct(string productName)
        {
            return this.Products
                .Keys
                .FirstOrDefault(p => p.Name.Equals(productName, System.StringComparison.InvariantCultureIgnoreCase));
        }

        public int GetProductQuantity(string productName)
        {
            var product = FindProduct(productName);

            if(product is null)
            {
                return 0;
            }

            return this.Products[product];
        }
    }
}
