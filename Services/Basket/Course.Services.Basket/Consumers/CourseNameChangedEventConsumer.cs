using Course.Services.Basket.Services;
using Course.Shared.Messages;
using MassTransit;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Course.Services.Basket.Consumers
{
    public class CourseNameChangedEventConsumer : IConsumer<CourseNameChangedEvent>
    {
        private readonly IRedisService _redisService;
        private readonly IBasketService _basketService;

        public CourseNameChangedEventConsumer(IRedisService redisService,
                                              IBasketService basketService)
        {
            _redisService = redisService;
            _basketService = basketService;
        }
        public async Task Consume(ConsumeContext<CourseNameChangedEvent> context)
        {

            var keys = await _redisService.GetKeys();

            foreach (var key in (RedisResult[])keys)
            {
                var basket = await _basketService.GetBasket(key.ToString());

                if (basket != null)
                {
                    basket.Data.basketItems.ForEach(x =>
                    {
                        x.CourseName = context.Message.UpdatedName;
                    });
                    await _basketService.SaveOrUpdateBasket(basket.Data);
                }
            }
        }



    }
}
