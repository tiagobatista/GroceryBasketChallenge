using System;
using System.Collections.Generic;
using System.Linq;
using CodingChallenge.DI;
using Crosscutting.Exceptions;
using Crosscutting.Util;
using Microsoft.Extensions.DependencyInjection;
using Service.Interface;

namespace CodingChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = GetServiceProvider();

            var basketService = serviceProvider.GetService<IBasketService>();

            var checkoutService = serviceProvider.GetService<ICheckoutService>();

            while (true)
            {
                try
                {
                    Console.WriteLine($"Introduce your articles by writting '{Constants.InputBeggining}' followed by the items separated by spaces (Type 'e' to leave):");

                    var input = Console.ReadLine();

                    if (input.Equals("e"))
                    {
                        break;
                    }

                    var productList = GetProductList(input);

                    var basket = basketService.GenerateBasket(productList);

                    var checkoutMessage = checkoutService.ProcessBasket(basket);

                    Console.WriteLine(checkoutMessage);
                }
                catch (ProductNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (InputException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static List<string> GetProductList(string input)
        {
            var trimmedInput = input?.Trim();

            if (trimmedInput.Length == default || trimmedInput is null)
            {
                throw new InputException(Constants.EmptyInputExceptionMessage);
            }

            var splittedInput = trimmedInput.Split(" ");

            var inputBeggining = splittedInput[0];

            if (!inputBeggining.Equals(Constants.InputBeggining))
            {
                throw new InputException(Constants.InvalidInputExceptionMessage);
            }

            var productList = splittedInput
                .Skip(1)
                .ToList(); //skip the input beggining 'PriceBasket'

            return productList;
        }

        private static ServiceProvider GetServiceProvider()
        {
            var serviceCollection = new ServiceCollection();

            return Bootstrapper.SetupIoC(serviceCollection);
        }
    }
}
