using DataAccess.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace eStore {
    public class Program {
        public static void Main(string[] args) {
            // Initial DBContext object
            eStoreContext eStore = new eStoreContext();
            // Print all products
            var products = from p in eStore.Products 
                           select new {p.ProductName, p.CategoryId};
            foreach (var product in products) {
                Console.WriteLine($"ProductName: {product.ProductName}, CategoryID: {product.CategoryId}");
            }

            Console.ReadLine();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
