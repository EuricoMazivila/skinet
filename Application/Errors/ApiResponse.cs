using System;
using System.Net;

namespace Application.Errors
{
    public class ApiResponse 
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        
        public ApiResponse(HttpStatusCode statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string GetDefaultMessageForStatusCode(HttpStatusCode statusCode)
        {
            return statusCode switch
            {
                HttpStatusCode.BadRequest => "A bad request, you have made",
                HttpStatusCode.Unauthorized => "Authorized, you are not",
                HttpStatusCode.NotFound => "Resource found, it was not",
                HttpStatusCode.InternalServerError => "Error are the path to the dark side. Errors lead to anger. Anger leads to hate. Hate leads to career change",
                _ => null
            };
        }
        
    }
}