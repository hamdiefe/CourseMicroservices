using StackExchange.Redis;

namespace Course.Services.Basket.Services
{
    public interface IRedisService
    {
        void Connect();
        IDatabase GetDatabase(int db = 1);
    }
}
