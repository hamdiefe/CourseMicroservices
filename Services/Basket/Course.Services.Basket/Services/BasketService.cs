using Course.Services.Basket.Dtos;
using Course.Shared.Dtos;
using System.Text.Json;
using System.Threading.Tasks;

namespace Course.Services.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly IRedisService _redisService;

        public BasketService(IRedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<Response<bool>> DeleteBasket(string userId)
        {
            var status = await _redisService.GetDatabase().KeyDeleteAsync(userId);

            return status ? Response<bool>.Success(204) : Response<bool>.Fail("Basket not found", 500);
        }

        public async Task<Response<BasketDto>> GetBasket(string userId)
        {
            var basket = await _redisService.GetDatabase().StringGetAsync(userId);

            if (string.IsNullOrEmpty(basket))
            {
                return Response<BasketDto>.Fail("Basket not found", 404);
            }

            return Response<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(basket), 200);
        }

        public async Task<Response<bool>> SaveOrUpdateBasket(BasketDto basketDto)
        {
            var status = await _redisService.GetDatabase().StringSetAsync(basketDto.UserId,JsonSerializer.Serialize(basketDto));

            return status ? Response<bool>.Success(204) : Response<bool>.Fail("Basket couldn't save or update",500);
        }
    }
}
