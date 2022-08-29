using Ordarat.DataAccessLayer.Entities;
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
    }
}
