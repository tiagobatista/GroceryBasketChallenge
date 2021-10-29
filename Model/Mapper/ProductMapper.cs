using Crosscutting.Exceptions;
using Crosscutting.Util;

namespace Domain.Model.Mapper
{
    public class ProductMapper : IProductMapper
    {
        public ProductDTO Map(Product product)
        {
            if(product == null)
            {
                return null;
            }

            if(string.IsNullOrEmpty(product.Name.Trim()) || product.Price <= 0)
            {
                throw new InvalidProductAttributeException(Constants.InvalidProductAttributeExceptionMessage);
            }

            return new ProductDTO
            {
                Name = product.Name,
                Price = product.Price
            };
        }
    }
}
