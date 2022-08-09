using Ordarat.BussniessLogicLayer.Interfaces;
using Ordarat.DataAccessLayer.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordarat.BussniessLogicLayer.Services
{
    public class TokenServices : ITokenServices
    {
        public Task<string> CreateToken(AppUser user)
        {
            throw new NotImplementedException();
        }
    }
}
