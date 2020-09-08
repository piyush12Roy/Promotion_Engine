using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PromotionEngine.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace PromotionEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            //Adding hosing method
            try
            {
                var builder = new ConfigurationBuilder();
                BuildConfig(builder);

                var host = Host.CreateDefaultBuilder()
                    .ConfigureServices((context, services) =>
                    {
                        services.AddSingleton<ICartOperations, CartOperations>();
                    })
                    .Build();

                //Initilizing input data
                List<SelectedCart> cartList = new List<SelectedCart>();
                cartList.Add(new SelectedCart { SKUIds = 'A', Quantity = 3 });
                cartList.Add(new SelectedCart { SKUIds = 'B', Quantity = 5 });
                cartList.Add(new SelectedCart { SKUIds = 'C', Quantity = 1 });
                cartList.Add(new SelectedCart { SKUIds = 'D', Quantity = 1 });

                //creating instance of CartOperations class
                var svc = ActivatorUtilities.CreateInstance<CartOperations>(host.Services);
                //Invoking checkout method
                int orderValue= svc.CheckOut(cartList);

                Console.WriteLine("Order value = {0} " , orderValue);

            }
            catch (Exception)
            {

                throw;
            }
           
        }
        static void BuildConfig(IConfigurationBuilder builder)
        {
            //configure appsetting.js
            builder.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        }
    }
}
