using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordarat.BussniessLogicLayer.Interfaces;
using Ordarat.DataAccessLayer.Entities;
using Ordarat.Errors;
using System.Threading.Tasks;

namespace Ordarat.Controllers
{
 
    public class PaymentController : BaseApiController
    {
        private readonly IPaymentServices _paymentService;

        public PaymentController(IPaymentServices paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost("{basketId")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            if(basket == null)
                return BadRequest(new ApiResponse(400 , "Problem With your Basket"));
            return Ok(basket);
        }
       
    }
}
