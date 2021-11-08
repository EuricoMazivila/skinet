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
        public class QueryTransactionStatusQuery : IRequest<QueryResponse>
        {
            public QueryRequest QueryRequest { get; set; }
        }
        
        public class QueryTransactionStatusValidator : AbstractValidator<QueryTransactionStatusQuery>
        {
            public QueryTransactionStatusValidator()
            {
                RuleFor(x => x.QueryRequest).NotEmpty();
            }
        }

        public class QueryTransactionStatusHandler : IRequestHandler<QueryTransactionStatusQuery, QueryResponse>
        {
            private readonly IPaymentMpesa _paymentMpesa;

            public QueryTransactionStatusHandler(IPaymentMpesa paymentMpesa)
            {
                _paymentMpesa = paymentMpesa;
            }
            
            public async Task<QueryResponse> Handle(QueryTransactionStatusQuery request, CancellationToken cancellationToken)
            {
                var queryTransactionStatus = await _paymentMpesa.QueryTransactionStatus(request.QueryRequest);

                var queryResponse = new QueryResponse
                {
                    IsSuccessfully = queryTransactionStatus.IsSuccessfully,
                    Description = queryTransactionStatus.Description,
                    TransactionStatus = queryTransactionStatus.TransactionStatus,
                    QueryRequest = request.QueryRequest
                };

                return queryResponse;
            }
        }
        
    }
}