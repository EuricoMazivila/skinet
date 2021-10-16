using System.Net;
using Application.Dtos;
using Application.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("errors/{code}")]
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(HttpStatusCode code)
        {
            var error = new ApiResponse(code);
            var errorReturn = new ApiResponseDto
            {
                ErrorMessage = error.ErrorMessage, 
                StatusCode = error.StatusCode
            };
            return new ObjectResult(errorReturn);
        }
    }
}