using Ordarat.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordarat.BussniessLogicLayer.Specification.ProductSpecification
{
    public class ProductWithFilterForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFilterForCountSpecification(ProductSpecParams productParams)
            : base(p =>
             (!productParams.BrandId.HasValue || productParams.BrandId == p.ProductBrandId) &&
             (!productParams.TypeId.HasValue || productParams.TypeId == p.ProductTypeId)
            )
        {

        }
    }
}
