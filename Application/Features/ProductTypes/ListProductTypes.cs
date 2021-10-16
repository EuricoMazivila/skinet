using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.Features.ProductTypes
{
    public class ListProductTypes 
    {
        public class ListProductTypesQuery : IRequest<IReadOnlyList<ProductType>>
        {
        }
        
        public class ListProductTypesHandler : IRequestHandler<ListProductTypesQuery, IReadOnlyList<ProductType>>
        {
            private readonly IGenericRepository<ProductType> _productTypeRepo;

            public ListProductTypesHandler(IGenericRepository<ProductType> productTypeRepo)
            {
                _productTypeRepo = productTypeRepo;
            }
            
            public async Task<IReadOnlyList<ProductType>> Handle(ListProductTypesQuery request, 
                CancellationToken cancellationToken)
            {
                return await _productTypeRepo.ListAllAsync();
            }
        }
        
    }
}