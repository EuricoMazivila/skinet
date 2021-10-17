using System;
using System.Net;

namespace Application.Errors
{
    public class ApiPaymentException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public ApiPaymentException(string code, string message)
        {
            ErrorMessage = message;
            StatusCode = GetHttpStatusCodeForCode(code);
        }
        
        private HttpStatusCode GetHttpStatusCodeForCode(string code)
        {
            return code switch
            {
                "INS-1" => HttpStatusCode.InternalServerError,
                "INS-2" => HttpStatusCode.Unauthorized,
                "INS-4" => HttpStatusCode.Unauthorized,
                "INS-5" => HttpStatusCode.Unauthorized,
                "INS-6" => HttpStatusCode.Unauthorized,
                "INS-9" => HttpStatusCode.RequestTimeout,
                "INS-10" => HttpStatusCode.Conflict,
                "INS-13" => HttpStatusCode.BadRequest,
                "INS-14" => HttpStatusCode.BadRequest,
                "INS-15" => HttpStatusCode.BadRequest,
                "INS-16" => HttpStatusCode.UnavailableForLegalReasons,
                "INS-17" => HttpStatusCode.BadRequest,
                "INS-18" => HttpStatusCode.BadRequest,
                "INS-19" => HttpStatusCode.BadRequest,
                "INS-20" => HttpStatusCode.BadRequest,
                "INS-21" => HttpStatusCode.BadRequest,
                "INS-22" => HttpStatusCode.BadRequest,
                "INS-23" => HttpStatusCode.BadRequest,
                "INS-24" => HttpStatusCode.BadRequest,
                "INS-25" => HttpStatusCode.BadRequest,
                "INS-26" => HttpStatusCode.BadRequest,
                "INS-993" => HttpStatusCode.BadRequest,
                "INS-994" => HttpStatusCode.BadRequest,
                "INS-995" => HttpStatusCode.BadRequest,
                "INS-996" => HttpStatusCode.BadRequest,
                "INS-997" => HttpStatusCode.BadRequest,
                "INS-998" => HttpStatusCode.BadRequest,
                "INS-2001" => HttpStatusCode.BadRequest,
                "INS-2002" => HttpStatusCode.BadRequest,
                "INS-2006" => HttpStatusCode.UnprocessableEntity,
                "INS-2051" => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };
        }
    }
}