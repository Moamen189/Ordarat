using Microsoft.Extensions.Logging;
using Ordarat.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ordarat.DataAccessLayer
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StroreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductType.Any())
                {
                    var typesData = File.ReadAllText("../Ordarat.DataAccessLayer/Data/Seeds/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    foreach (var type in types)
                        context.ProductType.Add(type);
                    await context.SaveChangesAsync();
                }

                if (!context.ProductBrand.Any())
                {
                    var barndsData = File.ReadAllText("../Ordarat.DataAccessLayer/Data/Seeds/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(barndsData);
                    foreach (var brand in brands)
                        context.ProductBrand.Add(brand);
                    await context.SaveChangesAsync();
                }

                if (!context.Product.Any())
                {
                    var ProductsData = File.ReadAllText("../Ordarat.DataAccessLayer/Data/Seeds/products.json");
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                    foreach (var Product in Products)
                        context.Product.Add(Product);
                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
               var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }


    }
}
