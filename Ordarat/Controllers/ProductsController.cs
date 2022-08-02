using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordarat.BussniessLogicLayer.Interfaces;
using Ordarat.BussniessLogicLayer.Specification;
using Ordarat.DataAccessLayer.Entities;
using Ordarat.Dtos;
using Ordarat.Errors;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ordarat.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _brandsRepo;
        private readonly IMapper _mapper;

        public IGenericRepository<ProductType> TypsRepo { get; }

        public ProductsController(IGenericRepository<Product> productRepo , IGenericRepository<ProductBrand> BrandsRepo,  IGenericRepository<ProductType> TypsRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _brandsRepo = BrandsRepo;
            this.TypsRepo = TypsRepo;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts(string sort)
        {

            var spec = new ProductWithTypesAndBrandsSpecification(sort);
            var products = await _productRepo.GetAllWithSpecAsync(spec);


            return Ok(_mapper.Map<IReadOnlyList<Product> , IReadOnlyList<ProductToReturnDto>>(products));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse) ,StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductWithTypesAndBrandsSpecification(id);
            var product = await _productRepo.GetWithSpecAsync(spec);
            var productDto = _mapper.Map<Product, ProductToReturnDto>(product);
            if(productDto == null)
                return NotFound(new ApiResponse(404));
            return Ok(productDto);
        }

        [HttpGet("brands")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            
            var brands = await _brandsRepo.GetAllAsync();


            return Ok(brands);
        }

        [HttpGet("types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTyps()
        {

            var typs = await TypsRepo.GetAllAsync();


            return Ok(typs);
        }
    }
}
