using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Features.ProductTypes;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductTypeController : BaseApiController
    {
        [HttpGet]
        public async Task<IReadOnlyList<ProductType>> ListProductTypes()
        {
            return await Mediator.Send(new ListProductTypes.ListProductTypesQuery());
        }
    }
}