using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordarat.BussniessLogicLayer.Interfaces;
using Ordarat.BussniessLogicLayer.Specification;
using Ordarat.BussniessLogicLayer.Specification.ProductSpecification;
using Ordarat.DataAccessLayer.Entities;
using Ordarat.Dtos;
using Ordarat.Errors;
using Ordarat.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ordarat.Controllers
{
    //[Authorize]
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

        public async Task<ActionResult<IReadOnlyList<Pagination<ProductToReturnDto>>>> GetProducts([FromQuery]ProductSpecParams productParams)
        {

            var spec = new ProductWithTypesAndBrandsSpecification(productParams);
            var countSpec = new ProductWithFilterForCountSpecification(productParams);
            var totalItem =await _productRepo.GetCountAsync(countSpec);
            var products = await _productRepo.GetAllWithSpecAsync(spec);

            var Data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
           
            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex , productParams.PageSize, totalItem, Data));
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
