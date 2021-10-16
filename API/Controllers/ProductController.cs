using System.Threading.Tasks;
using Application.Dtos;
using Application.Specifications;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Products;
using Application.Helpers;

namespace API.Controllers
{
    public class ProductController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> ListProducts(
            [FromQuery]ProductSpecParams productSpecParams)
        {
            return await Mediator.Send(new ListProducts.ListProductsQuery 
                {ProductSpecParams = productSpecParams});
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int productId)
        {
            return await Mediator.Send(new GetProductById.GetProductByIdQuery {ProductId = productId});
        }
    }
}