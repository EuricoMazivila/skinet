namespace Application.Helpers
{
    public class PaymentResponse
    {
        public bool IsSuccessfully { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string TransactionStatus { get; set; }
        
        
    }
}