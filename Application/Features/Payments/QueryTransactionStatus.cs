using System.Threading;
using System.Threading.Tasks;
using Application.Helpers;
using Application.Interfaces;
using FluentValidation;
using MediatR;

namespace Application.Features.Payments
{
    public class QueryTransactionStatus
    {
        public class QueryTransactionStatusQuery : IRequest<PaymentResponse>
        {
            public PaymentRequest PaymentRequest { get; set; }
        }
        
        public class QueryTransactionStatusValidator : AbstractValidator<QueryTransactionStatusQuery>
        {
            public QueryTransactionStatusValidator()
            {
                RuleFor(x => x.PaymentRequest).NotEmpty();
            }
        }

        public class QueryTransactionStatusHandler : IRequestHandler<QueryTransactionStatusQuery, PaymentResponse>
        {
            private readonly IPaymentMpesa _paymentMpesa;

            public QueryTransactionStatusHandler(IPaymentMpesa paymentMpesa)
            {
                _paymentMpesa = paymentMpesa;
            }
            
            public async Task<PaymentResponse> Handle(QueryTransactionStatusQuery request, CancellationToken cancellationToken)
            {
                var queryTransactionStatus = await _paymentMpesa.QueryTransactionStatus(request.PaymentRequest);

                var queryResponse = new PaymentResponse
                {
                    IsSuccessfully = queryTransactionStatus.IsSuccessfully,
                    Description = queryTransactionStatus.Description,
                    TransactionStatus = queryTransactionStatus.TransactionStatus
                };

                return queryResponse;
            }
        }
        
    }
}