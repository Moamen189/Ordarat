using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordarat.BussniessLogicLayer.Interfaces;
using Ordarat.DataAccessLayer.Entities;
using System.Threading.Tasks;

namespace Ordarat.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet]

        public async Task<ActionResult<CustomerBasket>> GetBasket(string basaketId)
        {
            var basket = await _basketRepository.GetCustomerBasket(basaketId);
            return Ok(basket ?? new CustomerBasket(basaketId));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            var customerbasket = await _basketRepository.UpdateCustomerBasket(basket);
            return Ok(customerbasket);
        }
        
        [HttpDelete]

        public async Task<ActionResult<bool>> DeleteBasket(string basketId)
        {
             return await _basketRepository.DeleteCustomerBasket(basketId);
        }


    }
}
