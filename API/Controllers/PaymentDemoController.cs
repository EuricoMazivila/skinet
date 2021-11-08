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
                .Amount(paymentReq.TotalPrice)
                .From($"{paymentReq.PhoneNumber}")
                .Reference(RandomStringGenerator.GetString())
                .Transaction("T12344A")
                .Build();

            var response = await client.Receive(paymentRequest);
                
            if (response.Code == "INS-0")
            {
                var paymentResponse = new PaymentResponse
                {
                    IsSuccessfully = response.IsSuccessfully,
                    Description = response.Description,
                    PaymentRequest = paymentReq
                };
                
                return paymentResponse;
            }
                
            throw new ApiPaymentException(response.Code, response.Description);
        }
        
        [HttpGet("queryStatus")]
        public async Task<ActionResult<QueryResponse>> QueryTransactionStatus([FromBody] QueryRequest queryReq)
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
                var queryResponse = new QueryResponse
                {
                    IsSuccessfully = response.IsSuccessfully,
                    Description = response.Description,
                    TransactionStatus =  response.TransactionStatus,
                    QueryRequest = queryReq
                };
                
                return queryResponse;
            }
                
            throw new ApiPaymentException(response.Code, response.Description);
        }
    }
}