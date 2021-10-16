using System.Net;

namespace Application.Dtos
{
    public class ApiResponseDto
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}