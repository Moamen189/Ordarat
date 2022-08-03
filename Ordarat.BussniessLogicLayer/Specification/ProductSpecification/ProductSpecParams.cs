using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordarat.BussniessLogicLayer.Specification.ProductSpecification
{
    public class ProductSpecParams
    {
        private const int PageMaxSize = 50;

        public int PageIndex { get; set; } = 1;

        private int pageSize = 1;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > PageMaxSize ? PageMaxSize : value; }
        }


        public string sort { get; set; }

        public int? TypeId { get; set; }

        public int? BrandId { get; set; }

        private string search;

        public string Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }

    }
}
