using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Ordarat.DataAccessLayer.Entities.Identity;
using Ordarat.DataAccessLayer.Identity;

namespace Ordarat.Extensions
{
    public static class IdentityServicesExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                

            }).AddEntityFrameworkStores<AppIdentityDbContext>();
            services.AddAuthentication();

            return services;
        }
    }
}
