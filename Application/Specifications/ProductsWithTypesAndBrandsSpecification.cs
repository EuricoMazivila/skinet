using Domain;

namespace Application.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(string sort, int? brandId, int? typeId)
            : base(x => 
            (!brandId.HasValue || x.ProductBrandId == brandId) && 
            (!typeId.HasValue || x.ProductTypeId == typeId)
            )
        {
            AddInclude(x=>x.ProductBrand);
            AddInclude(x=>x.ProductType);

            if(!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc": 
                        AddOrderByDescending(p => p.Price);
                        break; 
                    default:
                        AddOrderBy(n => n.Name);
                        break;    
                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id)
            : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);   
        }
    }
}