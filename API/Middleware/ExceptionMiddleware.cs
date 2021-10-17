using System.Net;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Application.Errors;
using System.Text.Json;
using Application.Dtos;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,
        IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);   
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";
            ApiException apiException = null;
            ApiPaymentException payException = null;
            int statusCode = 0;
            bool payment = false;

            switch (ex)
            {
                case ApiException apiEx: 
                    statusCode = (int) apiEx.StatusCode;
                    if (_env.IsDevelopment())
                    {
                        apiEx.Details = apiEx.StackTrace;
                        apiException = apiEx;
                    }
                    else
                    {
                        apiException= apiEx;
                    }
                    break;
                
                case ApiPaymentException payEx:
                    payment = true;
                    payException = payEx;
                    break;
                
                case { } e: 
                    statusCode = (int) HttpStatusCode.InternalServerError;
                    apiException = _env.IsDevelopment()
                        ? new ApiException((HttpStatusCode) statusCode, e.Message, e.StackTrace)
                        : new ApiException(HttpStatusCode.InternalServerError, e.Message);
                    break;
            }

            context.Response.StatusCode = statusCode;
            Object responseReturn = null;
            
            if (payment)
            {
                responseReturn = new ApiPaymentExceptionDto
                    {StatusCode = payException.StatusCode, ErrorMessage = payException.ErrorMessage};
            }
            else
            {
                 responseReturn = _env.IsDevelopment()
                    ? new ApiExceptionDto{StatusCode = apiException.StatusCode, ErrorMessage = apiException.ErrorMessage, 
                        Details = apiException.Details}
                    : new ApiExceptionDto{StatusCode = apiException.StatusCode, ErrorMessage = apiException.ErrorMessage };    
            }


            var options = new JsonSerializerOptions{ PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
            var json = JsonSerializer.Serialize(responseReturn, options);

            await context.Response.WriteAsync(json);
        }
    }
}