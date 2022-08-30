using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordarat.BussniessLogicLayer.Interfaces
{
    public interface IResponseCashService
    {
        Task CasheResponseAsync(string cacheKey , object response , TimeSpan timeToLive );
        Task<string> GetCashedResponse(string cacheKey);
    }
}
