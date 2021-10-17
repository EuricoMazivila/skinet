using System.Linq;
using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using Infrastructure.Data;
using Infrastructure.Payments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>),(typeof(GenericRepository<>)));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPaymentMpesa, PaymentMpesa>();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };
                    
                    var errorResponseReturn = new ApiValidationErrorResponseDto
                    {
                        Errors = errorResponse.Errors,
                        ErrorMessage = errorResponse.ErrorMessage,
                        StatusCode = errorResponse.StatusCode
                    };

                    return new BadRequestObjectResult(errorResponseReturn);
                    
                };
            });
            
            return services;
        }
    }
}