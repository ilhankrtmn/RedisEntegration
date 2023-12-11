using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisEntegration.Business.Interfaces
{
    public interface IRedisCacheServices
    {
        Task ClearAsync(string key);
        Task<string> GetValueAsync(string key);
        Task<bool> SetValueAsync(string key, string value);
        void ClearAll();
    }
}
