using System.Collections.Generic;
using System.Linq;
using Crosscutting.Exceptions;
using Crosscutting.Util;
using Domain.Model;
using Domain.Model.Mapper;
using Repository.Interface;

namespace Repository.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private static List<Product> products;

        private IProductMapper productMapper;

        public ProductRepository(IProductMapper productMapper)
        {
            this.productMapper = productMapper;

            var dataFile = Constants.DataFile;

            products = FileReader
                .GetJsonFileContent<IEnumerable<Product>>(dataFile)
                .ToList(); //For simplicity matters, I'm assuming the file values are valid
        }

        /// <summary>
        /// Retrieves the Product
        /// </summary>
        /// <param name="name">The product's name</param>
        /// <returns>The Product object</returns>
        public ProductDTO GetProduct(string name)
        {
            var fileProduct = products
                .FirstOrDefault(p => p.Name.Equals(name, System.StringComparison.InvariantCultureIgnoreCase));

            return productMapper.Map(fileProduct);
        }

        /// <summary>
        /// Updates a product stock. Doesn't update the data file so that we don't have to update it manually everytime we want to run the program.
        /// </summary>
        /// <param name="productName">The product to update name</param>
        /// <param name="productsTaken">The number of items taken from stock</param>
        public void UpdateProductStock(string productName, int productsTaken)
        {
            var productStock = products
                .FirstOrDefault(p => p.Name.Equals(productName, System.StringComparison.InvariantCultureIgnoreCase))
                .Stock; //code repeated because I want to update the mock db and we don't have stock in the DTO

            if (productsTaken > productStock)
            {
                throw new NotEnoughStockException(Constants.NotEnoughStockExceptionMessage);
            }

            productStock -= productsTaken;
        }
    }
}
