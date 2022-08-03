using Ordarat.BussniessLogicLayer.Specification.ProductSpecification;
using Ordarat.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordarat.BussniessLogicLayer.Specification.ProductSpecification
{
    public class ProductWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductWithTypesAndBrandsSpecification(ProductSpecParams productParams)
            :base(p => 
            (!productParams.BrandId.HasValue || productParams.BrandId == p.ProductBrandId) &&
            (!productParams.TypeId.HasValue || productParams.TypeId == p.ProductTypeId) 
            )
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1) , productParams.PageSize);
            AddOrderBy(p => p.Name);

            if (!string.IsNullOrEmpty(productParams.sort))
            {
                switch (productParams.sort)
                {
                    case "priceAsc":
                        AddOrderBy(P => P.Price);
                        break;

                    case "priceDsc":
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;

                }
            }
        }

        public ProductWithTypesAndBrandsSpecification(int id):base(P => P.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

        }
    }
}
