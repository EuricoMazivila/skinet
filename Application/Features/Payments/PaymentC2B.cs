using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Helpers;
using Application.Interfaces;
using FluentValidation;
using MediatR;

namespace Application.Features.Payments
{
    public class PaymentC2B
    {
        public class PaymentC2BCommand : IRequest<PaymentResponse>
        {
            public PaymentRequest PaymentRequest { get; set; }
        }
        
        public class PaymentC2BValidator : AbstractValidator<PaymentC2BCommand>
        {
            public PaymentC2BValidator()
            {
                RuleFor(x => x.PaymentRequest).NotEmpty();
            }
        }
        
        public class PaymentC2BHandler : IRequestHandler<PaymentC2BCommand, PaymentResponse>
        {
            private readonly IPaymentMpesa _paymentMpesa;

            public PaymentC2BHandler(IPaymentMpesa paymentMpesa)
            {
                _paymentMpesa = paymentMpesa;
            }
            
            public async Task<PaymentResponse> Handle(PaymentC2BCommand request, CancellationToken cancellationToken)
            {
                var c2BResponse = await _paymentMpesa.C2B(request.PaymentRequest);

                var paymentResponse = new PaymentResponse
                {
                    IsSuccessfully = c2BResponse.IsSuccessfully,
                    Description = c2BResponse.Description,
                    PaymentRequest = request.PaymentRequest
                };

                return paymentResponse;
            }
        }
    }
}