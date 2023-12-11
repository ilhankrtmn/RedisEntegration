using RedisEntegration.Business.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisEntegration.Business.Services
{
    public class RedisCacheServices : IRedisCacheServices
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly IDatabaseAsync _databaseAsync;

        public RedisCacheServices(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
            _databaseAsync = _connectionMultiplexer.GetDatabase();
        }

        public async Task ClearAsync(string key)
        {
            await _databaseAsync.KeyDeleteAsync(key);
        }

        public async Task<string> GetValueAsync(string key)
        {
            return await _databaseAsync.StringGetAsync(key);
        }

        public async Task<bool> SetValueAsync(string key, string value)
        {
            return await _databaseAsync.StringSetAsync(key, value);
        }

        public void ClearAll()
        {
            var redisEndpoints = _connectionMultiplexer.GetEndPoints(true);
            foreach (var redisEndpoint in redisEndpoints)
            {
                var redisServer = _connectionMultiplexer.GetServer(redisEndpoint);
                redisServer.FlushAllDatabases();
            }
        }
    }
}
