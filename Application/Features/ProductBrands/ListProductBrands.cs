using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.ProductBrands
{
    public class ListProductBrands
    {
        public class ListProductBrandsQuery: IRequest<IReadOnlyList<ProductBrand>>
        {
        }
        
        public class ListProductBrandsHandler : IRequestHandler<ListProductBrandsQuery, IReadOnlyList<ProductBrand>>
        {
            private readonly IGenericRepository<ProductBrand> _productBrandRepo;

            public ListProductBrandsHandler(IGenericRepository<ProductBrand> productBrandRepo)
            {
                _productBrandRepo = productBrandRepo;
            }
            
            public async Task<IReadOnlyList<ProductBrand>> Handle(ListProductBrandsQuery request, CancellationToken cancellationToken)
            {
                return await _productBrandRepo.ListAllAsync();
            }
        }
    }
}