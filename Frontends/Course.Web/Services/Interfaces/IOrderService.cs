using Course.Web.Models.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Course.Web.Services.Interfaces
{
    public interface IOrderService
    {

        // Senkron- direk order mikroservisine istek yapılacak
        Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput);

        // Asenkron- sipariş bilgileri rabbitMQ'ya 
        Task<OrderSuspendViewModel> SuspendOrder(CheckoutInfoInput checkoutInfoInput);

        Task<List<OrderViewModel>> GetOrder();
    }
}
