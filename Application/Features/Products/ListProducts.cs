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
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public ListProductsHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            
            public async Task<Pagination<ProductToReturnDto>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
            {
                var spec = new ProductsWithTypesAndBrandsSpecification(request.ProductSpecParams);

                var countSpec = new ProductWithFiltersForCountSpecification(request.ProductSpecParams);
                var totalItems = await _unitOfWork.Repository<Product>().CountAsync(countSpec);
            
                var products = await _unitOfWork.Repository<Product>().ListAllWithSpecAsync(spec);

                var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

                var pgProduct = new Pagination<ProductToReturnDto>(request.ProductSpecParams.PageIndex,
                    request.ProductSpecParams.PageSize, totalItems, data);
                
                return pgProduct;
            }
        }
    }
}