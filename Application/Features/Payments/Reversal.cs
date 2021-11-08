using System.Threading;
using System.Threading.Tasks;
using Application.Helpers;
using Application.Interfaces;
using FluentValidation;
using MediatR;

namespace Application.Features.Payments
{
    public class Reversal
    {
        public class ReversalCommand : IRequest<PaymentResponse>
        {
            public PaymentRequest PaymentRequest { get; set; }
        }
        
        public class ReversalValidator : AbstractValidator<ReversalCommand>
        {
            public ReversalValidator()
            {
                RuleFor(x => x.PaymentRequest).NotEmpty();
            }
        }
        
        public class ReversalHandler : IRequestHandler<ReversalCommand, PaymentResponse>
        {
            private readonly IPaymentMpesa _paymentMpesa;

            public ReversalHandler(IPaymentMpesa paymentMpesa)
            {
                _paymentMpesa = paymentMpesa;
            }
            
            public async Task<PaymentResponse> Handle(ReversalCommand request, CancellationToken cancellationToken)
            {
                var reversal = await _paymentMpesa.Reversal(request.PaymentRequest);
                
                var paymentResponse = new PaymentResponse
                {
                    IsSuccessfully = reversal.IsSuccessfully,
                    Description = reversal.Description,
                    Code = reversal.Code
                };

                return paymentResponse;
            }
        }
    }
}