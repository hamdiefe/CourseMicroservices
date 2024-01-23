using Course.Services.Basket.Dtos;
using Course.Services.Basket.Services;
using Course.Shared.ControllerBases;
using Course.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Course.Services.Basket.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BasketController : CustomBaseController
    {
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public BasketController(IBasketService basketService,
                                ISharedIdentityService sharedIdentityService)
        {
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            return CreateActionResultInstance(await _basketService.GetBasket(_sharedIdentityService.GetUserId));
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdateBasket(BasketDto basket)
        {
            basket.UserId = basket.UserId == "" ? _sharedIdentityService.GetUserId : basket.UserId;
            return CreateActionResultInstance(await _basketService.SaveOrUpdateBasket(basket));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket()
        {
            return CreateActionResultInstance(await _basketService.DeleteBasket(_sharedIdentityService.GetUserId));
        }
    }
}
