using System.Net;
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
            return new ObjectResult(new ApiResponse(code));
        }
    }
}