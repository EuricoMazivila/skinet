using System.Net;
using Application.Errors;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;
        public BuggyController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundResquest()
        {
            var thing = _context.Products.Find(42);

            if(thing == null)
            {
                return NotFound(new ApiResponse(HttpStatusCode.NotFound));
            }
            
            return Ok();   
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var thing = _context.Products.Find(42);

            var thingToReturn = thing.ToString();

            return Ok();   
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadResquest()
        {
            return BadRequest(new ApiResponse(HttpStatusCode.BadRequest));   
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return BadRequest();   
        }

        [HttpGet("noendpoint")]
        public ActionResult NoEndpoint()
        {
            return Ok();
        }
    }
}