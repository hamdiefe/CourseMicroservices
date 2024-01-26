using Course.Services.Discount.Dtos;
using Course.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Course.Services.Discount.Services
{
    public interface IDiscountService
    {
        Task<Response<List<DiscountDto>>> GetAll();

        Task<Response<DiscountDto>> GetById(int id);

        Task<Response<NoContent>> Save(DiscountCreateDto discount);

        Task<Response<NoContent>> Update(DiscountUpdateDto discount);

        Task<Response<NoContent>> Delete(int id);

        Task<Response<DiscountDto>> GetByCodeAndUserId(string code, string userId);
    }
}
