using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ordarat.DataAccessLayer.Entities.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ordarat.Extensions
{
    public static class UserManagerExtentions
    {
        public static async Task<AppUser> FindUserWithAddressByEmailAsync(this UserManager <AppUser> userManager, ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await userManager.Users.Include(U => U.Address).SingleOrDefaultAsync(U => U.Email == email);
            
            return user;
        }
    }
}
