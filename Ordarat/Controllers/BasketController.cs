using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordarat.BussniessLogicLayer.Interfaces;
using Ordarat.DataAccessLayer.Entities;
using Ordarat.Dtos;
using System.Threading.Tasks;

namespace Ordarat.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<ActionResult<CustomerBasket>> GetBasket(string basaketId)
        {
            var basket = await _basketRepository.GetCustomerBasket(basaketId);
            return Ok(basket ?? new CustomerBasket(basaketId));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var mappedBasket = _mapper.Map<CustomerBasketDto , CustomerBasket>(basket);
            var customerbasket = await _basketRepository.UpdateCustomerBasket(mappedBasket);
            return Ok(customerbasket);
        }
        
        [HttpDelete]

        public async Task<ActionResult<bool>> DeleteBasket(string basketId)
        {
             return await _basketRepository.DeleteCustomerBasket(basketId);
        }


    }
}
