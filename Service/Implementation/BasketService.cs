using System.Collections.Generic;
using Crosscutting.Exceptions;
using Crosscutting.Util;
using Repository.Interface;
using Service.Helper;
using Service.Interface;

namespace Service.Implementation
{
    public class BasketService : IBasketService
    {
        private IProductRepository productRepository;

        public BasketService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public Basket GenerateBasket(List<string> productList)
        {
            var basket = new Basket();

            //initializes the basket products and their quantities in the basket
            productList.ForEach(p =>
            {
                var product = this.productRepository.GetProduct(p) ?? throw new ProductNotFoundException(Constants.ProductNotFoundExceptionMessage); //Validates if it is a valid product

                if (!basket.Products.ContainsKey(product))
                {
                    basket.Products.Add(product, 1);
                }
                else
                {
                    basket.Products[product]++;
                }
            });

            //for each product, updates product stock based on the basket quantity
            foreach (var (key, value) in basket.Products)
            {
                this.productRepository.UpdateProductStock(key.Name, value);
            }

            return basket;
        }
    }
}
