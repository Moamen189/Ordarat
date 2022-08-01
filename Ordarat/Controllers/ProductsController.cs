using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordarat.BussniessLogicLayer.Interfaces;
using Ordarat.BussniessLogicLayer.Specification;
using Ordarat.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ordarat.Controllers
{
   
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;

        public ProductsController(IGenericRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpGet]

        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
        {
            var spec = new ProductWithTypesAndBrandsSpecification();
            var products = await _productRepo.GetAllWithSpecAsync(spec);


            return Ok(products);
        }
    }
}
