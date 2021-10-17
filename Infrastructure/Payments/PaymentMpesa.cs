using System.Threading.Tasks;
using Application.Helpers;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using MPesa;

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

            return response;
        }

        private Client Client()
        {
            var client = new Client.Builder()
                .ApiKey(_configuration["ApiKey"])
                .PublicKey(_configuration["PublicKey"])
                .ServiceProviderCode("171717")
                .InitiatorIdentifier("SJGW67fK")
                .Environment(Environment.Development)
                .SecurityCredential("Mpesa2019")
                .Build();
            
            return client;
        }
        
    }
}