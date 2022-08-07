using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ordarat.DataAccessLayer;
using Ordarat.DataAccessLayer.Entities.Identity;
using Ordarat.DataAccessLayer.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordarat
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var LoggerVactory = services.GetRequiredService<ILoggerFactory>();  
            try
            {
                var context = services.GetRequiredService<StroreContext>();
                await context.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(context, LoggerVactory);

                var IdentityContext = services.GetRequiredService<AppIdentityDbContext>();
                await IdentityContext.Database.MigrateAsync();

                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUserAsync(userManager);

            }
            catch (Exception ex)
            {
                var logger = LoggerVactory.CreateLogger<Program>(); //Console
                logger.LogError(ex, ex.Message);


            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
