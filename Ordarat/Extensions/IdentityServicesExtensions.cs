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

            }).AddEntityFrameworkStores<AppIdentityDbContext>();
            services.AddAuthentication();

            return services;
        }
    }
}
