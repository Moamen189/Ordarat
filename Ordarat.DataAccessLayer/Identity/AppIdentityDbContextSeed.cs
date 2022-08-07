using Microsoft.AspNetCore.Identity;
using Ordarat.DataAccessLayer.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordarat.DataAccessLayer.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManger)
        {
            if (!userManger.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Moamen Ashraf",
                    UserName = "Moamen189",
                    Email = "moamen.ashraf1892001@gmail.com",
                    PhoneNumber = "01001432236",
                    Address = new Address()
                    {
                        FirstName ="Moamen",
                        LastName = "Ashraf",
                        Country ="Eqypt",
                        City =  "Suez",
                        Streeet = "10 AlimamElshafie St."
                    }
                };

                await userManger.CreateAsync(user ,"Pa$$w0rd");
            }
        }
    }
}
