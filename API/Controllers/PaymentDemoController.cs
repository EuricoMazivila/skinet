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
        public async Task<ActionResult<PaymentResponse>> PaymentC2B([FromBody] PaymentRequest paymentReq)
        {
            var client = new Client.Builder()
                .ApiKey(_configuration["PaymentMpesa:ApiKey"])
                .PublicKey(_configuration["PaymentMpesa:PublicKey"])
                .ServiceProviderCode("171717")
                .InitiatorIdentifier("SJGW67fK")
                .Environment(Environment.Development)
                .SecurityCredential("Mpesa2019")
                .Build();
            
            //C2B
            var paymentRequest = new Request.Builder()
                .Amount(paymentReq.Amount)
                .From($"{paymentReq.PhoneNumber}")
                // .Reference(RandomStringGenerator.GetString())
                .Reference("WERWEWREWR")
                .Transaction("T12344A")
                .Build();

            var response = await client.Receive(paymentRequest);
                
            if (response.Code == "INS-0")
            {
                var paymentResponse = new PaymentResponse
                {
                    IsSuccessfully = response.IsSuccessfully,
                    Description = response.Description,
                    Code = response.Code
                };
                
                return paymentResponse;
            }
                
            throw new ApiPaymentException(response.Code, response.Description);
        }
        
        [HttpGet("queryStatus")]
        public async Task<ActionResult<PaymentResponse>> QueryTransactionStatus([FromBody] PaymentRequest queryReq)
        {
            var client = new Client.Builder()
                .ApiKey(_configuration["PaymentMpesa:ApiKey"])
                .PublicKey(_configuration["PaymentMpesa:PublicKey"])
                .ServiceProviderCode("171717")
                .InitiatorIdentifier("SJGW67fK")
                .Environment(Environment.Development)
                .SecurityCredential("Mpesa2019")
                .Build();
            
            //QueryTransaction
            var queryRequest = new Request.Builder()
                .Reference(queryReq.Reference)
                .Subject("T12344A")
                .Build();

            var response = await client.Query(queryRequest);
                
            if (response.Code == "INS-0")
            {
                var queryResponse = new PaymentResponse
                {
                    IsSuccessfully = response.IsSuccessfully,
                    Description = response.Description,
                    TransactionStatus =  response.TransactionStatus,
                    Code = response.Code
                };
                
                return queryResponse;
            }
                
            throw new ApiPaymentException(response.Code, response.Description);
        }
        
        [HttpPut("reversal")]
        public async Task<ActionResult<PaymentResponse>> Reversal([FromBody] PaymentRequest paymentReq)
        {
            var client = new Client.Builder()
                .ApiKey(_configuration["PaymentMpesa:ApiKey"])
                .PublicKey(_configuration["PaymentMpesa:PublicKey"])
                .ServiceProviderCode("171717")
                .InitiatorIdentifier("SJGW67fK")
                .Environment(Environment.Development)
                .SecurityCredential("Mpesa2019")
                .Build();
            
            //Reversal
            var paymentRequest = new Request.Builder()
                .Amount(paymentReq.Amount)
                .Reference(paymentReq.Reference)
                .Transaction(paymentReq.Transaction)
                .Build();

            var response = await client.Revert(paymentRequest);
                
            if (response.Code == "INS-0")
            {
                var paymentResponse = new PaymentResponse
                {
                    IsSuccessfully = response.IsSuccessfully,
                    Description = response.Description,
                    Code = response.Code
                };
                
                return paymentResponse;
            }
                
            throw new ApiPaymentException(response.Code, response.Description);
        }
        
    }
}