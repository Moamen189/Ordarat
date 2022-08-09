using Ordarat.DataAccessLayer.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordarat.BussniessLogicLayer.Interfaces
{
    public interface ITokenServices
    {
        Task<string> CreateToken(AppUser user);
    }
}
