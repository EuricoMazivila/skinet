using System.Net;

namespace Application.Dtos
{
    public class ApiPaymentExceptionDto
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}