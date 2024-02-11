using StackExchange.Redis;
using System.Threading.Tasks;

namespace Course.Services.Basket.Services
{
    public interface IRedisService
    {
        void Connect();
        IDatabase GetDatabase(int db = 1);
        Task<RedisResult> GetKeys();
    }
}
