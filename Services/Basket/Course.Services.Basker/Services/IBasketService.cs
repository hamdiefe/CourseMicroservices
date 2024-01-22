using Course.Services.Basket.Dtos;
using Course.Shared.Dtos;
using System.Threading.Tasks;

namespace Course.Services.Basket.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasket(string userId);

        Task<Response<bool>> SaveOrUpdateBasket(BasketDto basketDto);

        Task<Response<bool>> DeleteBasket(string userId);
    }
}
