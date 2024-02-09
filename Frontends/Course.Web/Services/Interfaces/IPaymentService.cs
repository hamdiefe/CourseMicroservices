using Course.Web.Models.Payment;
using System.Threading.Tasks;

namespace Course.Web.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput);

    }
}
