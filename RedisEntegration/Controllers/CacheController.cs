using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisEntegration.Business.Interfaces;

namespace RedisEntegration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private readonly IRedisCacheServices _redisCacheServices;

        public CacheController(IRedisCacheServices redisCacheServices)
        {
            _redisCacheServices = redisCacheServices;
        }

        [HttpPost("cache/{key}")]
        public async Task<IActionResult> GetData(string key)
        {
            return Ok(await _redisCacheServices.GetValueAsync(key));
        }

        [HttpPost("cache/set")]
        public async Task<IActionResult> SetData(string key, string value)
        {
            await _redisCacheServices.SetValueAsync(key, value);
            return Ok();
        }

        [HttpPost("cache/delete/{key}")]
        public async Task<IActionResult> Delete(string key)
        {
            await _redisCacheServices.ClearAsync(key);
            return Ok();
        }

        [HttpPost("cache/delete/all")]
        public async Task<IActionResult> DeleteAll()
        {
            _redisCacheServices.ClearAll();
            return Ok();
        }

    }
}
