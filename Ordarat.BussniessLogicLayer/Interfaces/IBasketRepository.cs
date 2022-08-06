using Ordarat.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordarat.BussniessLogicLayer.Interfaces
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetCustomerBasket(string basketId);

        Task<CustomerBasket> UpdateCustomerBasket(CustomerBasket basket);

        Task<bool> DeleteCustomerBasket(string basketId);

    }
}
