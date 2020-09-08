using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using PromotionEngine.Model;
using PromotionEngine.Promotions;
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
                    services.AddSingleton<IPromotion, Promotion>();
                })
                .Build();
        }
        static void BuildConfig(IConfigurationBuilder builder)
        {
            //configure appsetting.js
            builder.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        }

        [Ignore("Basic test for checkout method")]
        [Test]
        public void CheckOut_Method_Basic_Test_Return_0()
        {
            //Arrange
            //var svc = ActivatorUtilities.CreateInstance<CartOperations>(host.Services);

            ////Act
            //int result = svc.CheckOut();

            ////Assert

            //Assert.AreEqual(result, 0);


        }
        
        [Ignore("Basic test for promotion method")]
        [Test]
        public void CheckOut_Method_Return_With_No_Active_Promotion()
        { 
            // Arrange
            List<SelectedCart> cartList = new List<SelectedCart>();
            cartList.Add(new SelectedCart { SKUIds = 'A', Quantity = 3 });
            cartList.Add(new SelectedCart { SKUIds = 'B', Quantity = 5 });
            cartList.Add(new SelectedCart { SKUIds = 'C', Quantity = 1 });
            cartList.Add(new SelectedCart { SKUIds = 'D', Quantity = 1 });

            var svc = ActivatorUtilities.CreateInstance<CartOperations>(host.Services);

            //Act
            int result = svc.CheckOut(cartList);

            //Assert
           
            Assert.AreEqual(result, 0);


        }
        [Test]
        public void CheckOut_Method_Return_for_Active_Promotion_for_SkuID_A()
        {
            // Arrange
            List<SelectedCart> cartList = new List<SelectedCart>();
            cartList.Add(new SelectedCart { SKUIds = 'A', Quantity = 3 });
            

            var svc = ActivatorUtilities.CreateInstance<CartOperations>(host.Services);

            //Act
            int result = svc.CheckOut(cartList);

            //Assert

            Assert.AreEqual(result, 130);


        }
        [Test]
        public void CheckOut_Method_Return_for_Active_Promotion_for_SkuID_B()
        {
            // Arrange
            List<SelectedCart> cartList = new List<SelectedCart>();
            cartList.Add(new SelectedCart { SKUIds = 'B', Quantity = 2 });


            var svc = ActivatorUtilities.CreateInstance<CartOperations>(host.Services);

            //Act
            int result = svc.CheckOut(cartList);

            //Assert

            Assert.AreEqual(result, 45);


        }

        [Test]
        public void CheckOut_Method_Return_for_Active_Promotion_for_SkuID_C_D()
        {
            // Arrange
            List<SelectedCart> cartList = new List<SelectedCart>();
            cartList.Add(new SelectedCart { SKUIds = 'C', Quantity = 2 });
            cartList.Add(new SelectedCart { SKUIds = 'D', Quantity = 2 });


            var svc = ActivatorUtilities.CreateInstance<CartOperations>(host.Services);

            //Act
            int result = svc.CheckOut(cartList);

            //Assert

            Assert.AreEqual(result, 60);


        }
        [Test]
        public void ScenarioA()
        {
            // Arrange
            List<SelectedCart> cartList = new List<SelectedCart>();
            cartList.Add(new SelectedCart { SKUIds = 'A', Quantity = 1 });
            cartList.Add(new SelectedCart { SKUIds = 'B', Quantity = 1 });
            cartList.Add(new SelectedCart { SKUIds = 'C', Quantity = 1 });


            var svc = ActivatorUtilities.CreateInstance<CartOperations>(host.Services);

            //Act
            int result = svc.CheckOut(cartList);

            //Assert

            Assert.AreEqual(result, 100);


        }
        [Test]
        public void ScenarioB()
        {
            // Arrange
            List<SelectedCart> cartList = new List<SelectedCart>();
            cartList.Add(new SelectedCart { SKUIds = 'A', Quantity = 5 });
            cartList.Add(new SelectedCart { SKUIds = 'B', Quantity = 5 });
            cartList.Add(new SelectedCart { SKUIds = 'C', Quantity = 1 });


            var svc = ActivatorUtilities.CreateInstance<CartOperations>(host.Services);

            //Act
            int result = svc.CheckOut(cartList);

            //Assert

            Assert.AreEqual(result, 370);


        }
    }
}
