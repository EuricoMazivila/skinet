using System.Threading.Tasks;
using Application.Helpers;
using MPesa;

namespace Application.Interfaces
{
    public interface IPaymentMpesa
    {
        Task<Response> C2B(PaymentRequest paymentRequest);
    }
}