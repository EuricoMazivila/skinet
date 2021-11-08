namespace Application.Helpers
{
    public class PaymentRequest
    {
        public double Amount { get; set; }
        public string PhoneNumber { get; set; }
        public string Reference { get; set; }
        public string Transaction { get; set; }
        // public string Subject { get; set; }
    }
}