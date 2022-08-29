using Microsoft.Extensions.Configuration;
using Ordarat.BussniessLogicLayer.Interfaces;
using Ordarat.DataAccessLayer.Entities;
using Ordarat.DataAccessLayer.Entities.Order_Aggregate;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Product = Ordarat.DataAccessLayer.Entities.Product;

namespace Ordarat.BussniessLogicLayer.Services
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentServices(IConfiguration configuration, IBasketRepository basketRepository , IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }

       

        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:Secretkey"];
            var basket = await _basketRepository.GetCustomerBasket(basketId);
            if (basketId == null)
                return null;

            var shippingPrice = 0m;

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DelivaryMethod>().GetAsync(basket.DeliveryMethodId.Value);
                shippingPrice = deliveryMethod.Cost;
            }

            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.Repository<Product>().GetAsync(item.Id);
                if(item.Price != product.Price)
                    item.Price = product.Price;
            }

            var services = new PaymentIntentService();
            PaymentIntent intent;

            if(string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)(basket.Items.Sum(i => i.Quantity * (i.Price * 100))) + ((long)(shippingPrice * 100)),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                intent = await services.CreateAsync(options);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)(basket.Items.Sum(i => i.Quantity * (i.Price * 100))) + ((long)(shippingPrice * 100)),
                };
                await services.UpdateAsync(basket.PaymentIntentId, options);
            }

            basket.ShippingPrice = shippingPrice;

            await _basketRepository.UpdateCustomerBasket(basket);

            return basket;

            
        }
    }
}
