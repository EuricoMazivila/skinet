using System.Threading.Tasks;
using Application.Features.Payments;
using Application.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PaymentController : BaseApiController
    {
        [HttpPost("c2b")]
        public async Task<ActionResult<PaymentResponse>> PayBasket([FromBody] PaymentRequest paymentRequest)
        {
            return await Mediator.Send(new PaymentC2B.PaymentC2BCommand {PaymentRequest = paymentRequest});
        }
    }
}