﻿using StackExchange.Redis;

namespace Course.Services.Basket.Services
{
    public class RedisService : IRedisService
    {
        private readonly string _host;
        private readonly int _port;

        private ConnectionMultiplexer _ConnectionMultiplexer;

        public RedisService(string host, int port)
        {
            _host = host;
            _port = port;
        }

        public void Connect()
        {
            _ConnectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");
        }

        public IDatabase GetDatabase(int db = 1)
        {
            var result = _ConnectionMultiplexer.GetDatabase(db);
            return result;
        }
    }
}
