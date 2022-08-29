using Ordarat.DataAccessLayer.Entities;
using Ordarat.DataAccessLayer.Entities.Order_Aggregate;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordarat.BussniessLogicLayer.Interfaces
{
    public interface IPaymentServices
    {
        Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);
        Task<Order> UpdateOrderPaymentSucceded(string paymentIntentId);
    }
}
