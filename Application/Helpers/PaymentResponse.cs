namespace Application.Helpers
{
    public class PaymentResponse
    {
        public bool IsSuccessfully { get; set; }
        public string Description { get; set; }
        public PaymentRequest PaymentRequest { get; set; }
    }
}