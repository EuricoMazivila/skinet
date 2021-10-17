using System;
using System.Net;
using System.Threading.Tasks;
using Application.Errors;
using Application.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MPesa;
using Environment = MPesa.Environment;

namespace API.Controllers
{
    public class PaymentDemoController : BaseApiController
    {
        private readonly IConfiguration _configuration;

        public PaymentDemoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("c2b")]
        public async Task<ActionResult<PaymentResponse>> PayBasket([FromBody] PaymentRequest paymentReq)
        {
            var client = new Client.Builder()
                .ApiKey(_configuration["ApiKey"])
                .PublicKey(_configuration["PublicKey"])
                .ServiceProviderCode("171717")
                .InitiatorIdentifier("SJGW67fK")
                .Environment(Environment.Development)
                .SecurityCredential("Mpesa2019")
                .Build();
            
            //C2B
            var paymentRequest = new Request.Builder()
                .Amount(paymentReq.TotalPrice)
                .From($"{paymentReq.PhoneNumber}")
                .Reference(RandomStringGenerator.GetString())
                .Transaction("T12344A")
                .Build();

            try
            {
                var response = await client.Receive(paymentRequest);
                var paymentResponse = new PaymentResponse
                {
                    IsSuccessfully = response.IsSuccessfully,
                    Description = response.Description,
                    PaymentRequest = paymentReq
                };
                
                return paymentResponse;
            }
            catch (Exception e)
            {
                throw new ApiException(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}