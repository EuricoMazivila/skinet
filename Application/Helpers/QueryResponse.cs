namespace Application.Helpers
{
    public class QueryResponse
    {
        public bool IsSuccessfully { get; set; }
        public string Description { get; set; }
        public string TransactionStatus { get; set; }
        public QueryRequest QueryRequest { get; set; }
    }
}