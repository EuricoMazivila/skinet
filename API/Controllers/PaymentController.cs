using System.Threading.Tasks;
using Application.Features.Payments;
using Application.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PaymentController : BaseApiController
    {
        [HttpPost("c2b")]
        public async Task<ActionResult<PaymentResponse>> PaymentC2B([FromBody] PaymentRequest paymentRequest)
        {
            return await Mediator.Send(new PaymentC2B.PaymentC2BCommand {PaymentRequest = paymentRequest});
        }

        [HttpGet("queryStatus")]
        public async Task<ActionResult<PaymentResponse>> QueryTransactionStatus([FromBody] PaymentRequest queryRequest)
        {
            return await Mediator.Send(new QueryTransactionStatus.QueryTransactionStatusQuery
                {PaymentRequest = queryRequest});
        }

        [HttpPut("reversal")]
        public async Task<ActionResult<PaymentResponse>> Reversal([FromBody] PaymentRequest paymentRequest)
        {
            return await Mediator.Send(new Reversal.ReversalCommand {PaymentRequest = paymentRequest});
        }
    }
}