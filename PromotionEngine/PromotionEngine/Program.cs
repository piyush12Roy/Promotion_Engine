using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace PromotionEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            //Adding hosing method

            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                })
                .Build();
        }
        static void BuildConfig(IConfigurationBuilder builder)
        {
            //configure appsetting.js
            builder.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        }
    }
}
