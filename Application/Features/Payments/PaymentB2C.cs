using System.Threading;
using System.Threading.Tasks;
using Application.Helpers;
using Application.Interfaces;
using FluentValidation;
using MediatR;

namespace Application.Features.Payments
{
    public class PaymentB2C
    {
        public class PaymentB2CCommand : IRequest<PaymentResponse>
        {
            public PaymentRequest PaymentRequest { get; set; }

            public class PaymentB2CValidator : AbstractValidator<PaymentB2CCommand>
            {
                public PaymentB2CValidator()
                {
                    RuleFor(x => x.PaymentRequest).NotEmpty();
                }
            }

            public class PaymentB2CHandler : IRequestHandler<PaymentB2CCommand, PaymentResponse>
            {
                private readonly IPaymentMpesa _paymentMpesa;

                public PaymentB2CHandler(IPaymentMpesa paymentMpesa)
                {
                    _paymentMpesa = paymentMpesa;
                }

                public async Task<PaymentResponse> Handle(PaymentB2CCommand request,
                    CancellationToken cancellationToken)
                {
                    var b2CResponse = await _paymentMpesa.B2C(request.PaymentRequest);
                    var paymentResponse = new PaymentResponse
                    {
                        IsSuccessfully = b2CResponse.IsSuccessfully,
                        Description = b2CResponse.Description,
                        Code = b2CResponse.Code
                    };

                    return paymentResponse;
                }
            }
        }
    }
}