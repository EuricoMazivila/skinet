using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using Application.Specifications;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.Products
{
    public class GetProductById 
    {
        public class GetProductByIdQuery : IRequest<ProductToReturnDto>
        {
            public int ProductId { get; set; }
        }

        public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductToReturnDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public GetProductByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            
            public async Task<ProductToReturnDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                var spec = new ProductsWithTypesAndBrandsSpecification(request.ProductId);

                var product = await _unitOfWork.Repository<Product>().GetEntityWithSpec(spec);

                if (product == null)
                    throw new ApiException(HttpStatusCode.NotFound,$"Product with id {request.ProductId} not found");

                return _mapper.Map<Product, ProductToReturnDto>(product);
            }
        }
    }
}