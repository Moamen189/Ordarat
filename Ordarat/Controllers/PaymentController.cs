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
using Microsoft.Extensions.Logging;

namespace Ordarat.Controllers
{
 
    public class PaymentController : BaseApiController
    {
        private readonly IPaymentServices _paymentService;
        private readonly ILogger _logger;

        public PaymentController(IPaymentServices paymentService , ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
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
                        _logger.LogInformation("Congrats Payment Succeded");
                        intent = (PaymentIntent)stripeEvent.Data.Object;
                        order = await _paymentService.UpdateOrderPaymentSucceded(intent.Id);
                        break;
                    case Events.PaymentIntentPaymentFailed:
                        intent = (PaymentIntent)stripeEvent.Data.Object;
                        order = await _paymentService.UpdateOrderPaymentFailed(intent.Id);
                        _logger.LogInformation("Sorry Payment Succeded" , order.Id);
                        _logger.LogInformation("Sorry Payment Succeded", intent.Id);

                        break;
                    default:
                        break;
                }
                return new EmptyResult();

            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }
    }
}
