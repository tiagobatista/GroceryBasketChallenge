using Domain.Model.Mapper;
using Microsoft.Extensions.DependencyInjection;
using Repository.Implementation;
using Repository.Interface;
using Service.Implementation;
using Service.Interface;
using Service.Strategy;

namespace CodingChallenge.DI
{
    public static class Bootstrapper
    {
        public static ServiceProvider SetupIoC(IServiceCollection serviceCollection)
        {
            //Register services
            serviceCollection.AddSingleton<IBasketService, BasketService>();
            serviceCollection.AddSingleton<ICheckoutService, CheckoutService>();

            //Register strategies
            serviceCollection.AddSingleton(new IPromotionStrategy[]
            {
                new AppleImmediatePercentagePromotionStrategy(),
                new LoafOfBreadPromotionStrategy()
            });

            //Register mappers
            serviceCollection.AddSingleton<IProductMapper, ProductMapper>();

            //Register repositories
            serviceCollection.AddSingleton<IProductRepository, ProductRepository>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
