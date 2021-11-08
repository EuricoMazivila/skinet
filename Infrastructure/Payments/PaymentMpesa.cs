using System;
using System.Net;
using System.Threading.Tasks;
using Application.Errors;
using Application.Helpers;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using MPesa;
using Environment = MPesa.Environment;

namespace Infrastructure.Payments
{
    public class PaymentMpesa : IPaymentMpesa
    {
        private readonly IConfiguration _configuration;

        public PaymentMpesa(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public async Task<Response> C2B(PaymentRequest paymentReq)
        {
            var client = Client();

            var paymentRequest = new Request.Builder()
                .Amount(paymentReq.TotalPrice)
                .From($"{paymentReq.PhoneNumber}")
                .Reference(RandomStringGenerator.GetString())
                .Transaction("T12344A")
                .Build();

            var response = await client.Receive(paymentRequest);
                
            if (response.Code == "INS-0")
            {
                return response;
            }
                
            throw new ApiPaymentException(response.Code, response.Description);
        }

        private Client Client()
        {
            var client = new Client.Builder()
                .ApiKey(_configuration["PaymentMpesa:ApiKey"])
                .PublicKey(_configuration["PaymentMpesa:PublicKey"])
                .ServiceProviderCode("171717")
                .InitiatorIdentifier("SJGW67fK")
                .Environment(Environment.Development)
                .SecurityCredential("Mpesa2019")
                .Build();
            
            return client;
        }
    }
}