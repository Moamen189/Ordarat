using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordarat.BussniessLogicLayer.Interfaces;
using Ordarat.DataAccessLayer.Entities;
using Ordarat.Errors;
using Stripe;
using System.IO;
using System;
using System.Threading.Tasks;
using Ordarat.DataAccessLayer.Entities.Order_Aggregate;

namespace Ordarat.Controllers
{
 
    public class PaymentController : BaseApiController
    {
        private readonly IPaymentServices _paymentService;

        public PaymentController(IPaymentServices paymentService)
        {
            _paymentService = paymentService;
        }
        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            if(basket == null)
                return BadRequest(new ApiResponse(400 , "Problem With your Basket"));
            return Ok(basket);
        }


        [HttpPost("webhook")] //event state
        public async Task<ActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], "");
                PaymentIntent intent;
                Order order;
                switch (stripeEvent.Type)
                {
                    case Events.PaymentIntentSucceeded:
                        intent = (PaymentIntent)stripeEvent.Data.Object;
                        order = await _paymentService.UpdateOrderPaymentSeucceded(intent.Id);
                        break;
                    case Events.PaymentIntentPaymentFailed:
                        break;
                    default:
                        break;
                }

            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }
    }
}
