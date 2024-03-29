﻿using System.Threading.Tasks;
using Application.Helpers;
using MPesa;

namespace Application.Interfaces
{
    public interface IPaymentMpesa
    {
        Task<Response> C2B(PaymentRequest paymentRequest);
        Task<Response> B2C(PaymentRequest paymentRequest);
        Task<Response> QueryTransactionStatus(PaymentRequest queryRequest);
        Task<Response> Reversal(PaymentRequest paymentRequest);
    }
}