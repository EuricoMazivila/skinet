using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Helpers;
using Application.Interfaces;
using Application.Specifications;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.Products
{
    public class ListProducts
    {
        public class ListProductsQuery : IRequest<Pagination<ProductToReturnDto>>
        {
            public ProductSpecParams ProductSpecParams { get; set; }
        }
        
        public class ListProductsHandler : IRequestHandler<ListProductsQuery, Pagination<ProductToReturnDto>>
        {
            private readonly IGenericRepository<Product> _productsRepo;
            private readonly IMapper _mapper;

            public ListProductsHandler(IGenericRepository<Product> productsRepo, IMapper mapper)
            {
                _productsRepo = productsRepo;
                _mapper = mapper;
            }
            
            public async Task<Pagination<ProductToReturnDto>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
            {
                var spec = new ProductsWithTypesAndBrandsSpecification(request.ProductSpecParams);

                var countSpec = new ProductWithFiltersForCountSpecification(request.ProductSpecParams);
                var totalItems = await _productsRepo.CountAsync(countSpec);
            
                var products = await _productsRepo.ListAsync(spec);

                var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

                var pgProduct = new Pagination<ProductToReturnDto>(request.ProductSpecParams.PageIndex,
                    request.ProductSpecParams.PageSize, totalItems, data);
                
                return pgProduct;
            }
        }
    }
}