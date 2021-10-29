using Domain.Model;

namespace Repository.Interface
{
    public interface IProductRepository
    {
        /// <summary>
        /// Retrieves a product
        /// </summary>
        /// <param name="name">The product's name</param>
        /// <returns>A product with information about its name and price</returns>
        ProductDTO GetProduct(string name);

        /// <summary>
        /// Updates the Product stock
        /// </summary>
        /// <param name="name">Name of the product to update</param>
        /// <param name="stockTaken">Number of items that were taken from the current stock</param>
        void UpdateProductStock(string name, int stockTaken);
    }
}
