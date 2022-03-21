using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using TicketSystem.Api.Entities;
using TicketSystem.Api.Models;

namespace TicketSystem.Api.Utils
{
    public class RedisUtil
    {
        private readonly IDistributedCache _distributedCache;

        public RedisUtil(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
        }
        public void RedisSave(string cacheKey,object o)
        {
            var serializedList = JsonConvert.SerializeObject(o);
            var redisBookerByte = Encoding.UTF8.GetBytes(serializedList);
            var options = new DistributedCacheEntryOptions()
                //缓存对象绝对过期时间
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(5))
                //如果在定义的时间段内没有被请求，它将作为缓存对象过期。滑动到期应始终设置为低于绝对到期时间
                .SetSlidingExpiration(TimeSpan.FromMinutes(2));

            _distributedCache.SetAsync(cacheKey, redisBookerByte, options);
        }

        public string RedisRead(byte[] redisByte)
        {
            return Encoding.UTF8.GetString(redisByte);
        }

        public void RedisRemove(string cacheKey)
        {
            _distributedCache.RemoveAsync(cacheKey);
        }
    }
}
