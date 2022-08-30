using Newtonsoft.Json;
using Ordarat.BussniessLogicLayer.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Ordarat.BussniessLogicLayer.Repository
{
    public class ResponseCashService : IResponseCashService
    {
        private readonly IDatabase _database;

        public ResponseCashService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task CasheResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
        {
            if (response == null)
                return;
            var options = new JsonSerializerOptions () { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var serialzeResponse = JsonSerializer.Serialize(response , options);
            await _database.StringSetAsync(cacheKey, serialzeResponse , timeToLive);
        }

        public async Task<string> GetCashedResponse(string cacheKey)
        {
           var cachedResponse = await _database.StringGetAsync(cacheKey);
            if(cachedResponse.IsNullOrEmpty)
                return null;
            return cachedResponse;
        }
    }
}
