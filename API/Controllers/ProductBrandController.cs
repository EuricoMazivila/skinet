using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Features.ProductBrands;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductBrandController : BaseApiController
    {
        [HttpGet]
        public async Task<IReadOnlyList<ProductBrand>> ListProductBrands()
        {
            return await Mediator.Send(new ListProductBrands.ListProductBrandsQuery());
        }
    }
}