using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordarat.BussniessLogicLayer.Interfaces;
using Ordarat.BussniessLogicLayer.Specification;
using Ordarat.DataAccessLayer.Entities;
using Ordarat.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ordarat.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _brandsRepo;
        private readonly IMapper _mapper;

        public IGenericRepository<ProductType> _TypsRepo { get; }

        public ProductsController(IGenericRepository<Product> productRepo , IGenericRepository<ProductBrand> BrandsRepo,  IGenericRepository<ProductType> TypsRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _brandsRepo = BrandsRepo;
            _TypsRepo = TypsRepo;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductWithTypesAndBrandsSpecification();
            var products = await _productRepo.GetAllWithSpecAsync(spec);


            return Ok(_mapper.Map<IReadOnlyList<Product> , IReadOnlyList<ProductToReturnDto>>(products));
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductWithTypesAndBrandsSpecification(id);
            var product = await _productRepo.GetWithSpecAsync(spec);
            return Ok(_mapper.Map <Product , ProductToReturnDto>(product));
        }

        [HttpGet("brands")]

        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            
            var brands = await _brandsRepo.GetAllAsync();


            return Ok(brands);
        }

        [HttpGet("types")]

        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTyps()
        {

            var typs = await _TypsRepo.GetAllAsync();


            return Ok(typs);
        }
    }
}
