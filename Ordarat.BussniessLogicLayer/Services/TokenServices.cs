using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Ordarat.BussniessLogicLayer.Interfaces;
using Ordarat.DataAccessLayer.Entities.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ordarat.BussniessLogicLayer.Services
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration configuration;

        public TokenServices(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<string> CreateToken(AppUser user, UserManager<AppUser> userManager)
        {
            var authClaims = new List<Claim>()
           {
               new Claim(ClaimTypes.Email, user.Email),
               new Claim(ClaimTypes.GivenName , user.DisplayName)
           };

            var userRoles = await userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
                authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(double.Parse(configuration["JWT:DurationInDays"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature));
            return new JwtSecurityTokenHandler().WriteToken(token);
 


        }
    }
}
