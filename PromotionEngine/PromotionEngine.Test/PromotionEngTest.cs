using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using PromotionEngine.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace PromotionEngine.Test
{
    public class PromotionEngTest
    {
        IHost host;
        public PromotionEngTest()
        {
            //Adding hosing method

            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

             host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<ICartOperations, CartOperations>();
                })
                .Build();
        }
        static void BuildConfig(IConfigurationBuilder builder)
        {
            //configure appsetting.js
            builder.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        }
        [Test]
        public void CheckOut_Method_Basic_Test_Return_0()
        {
            //Arrange
            var svc = ActivatorUtilities.CreateInstance<CartOperations>(host.Services);

            //Act
            int result = svc.CheckOut();

            //Assert

            Assert.AreEqual(result, 0);


        }
    }
}
