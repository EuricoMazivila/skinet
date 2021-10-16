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
            private readonly IUnitOfWork _unitOfWork;

            public ListProductBrandsHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            
            public async Task<IReadOnlyList<ProductBrand>> Handle(ListProductBrandsQuery request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.Repository<ProductBrand>().ListAllAsync();
            }
        }
    }
}