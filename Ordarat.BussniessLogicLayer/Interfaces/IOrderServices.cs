using Ordarat.DataAccessLayer.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordarat.BussniessLogicLayer.Interfaces
{
    public interface IOrderServices
    {
        Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address ShipToAddress);
        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
        Task<Order> GetOrdersbyIdForUser(int orderId , string buyerEmail);

        Task<IReadOnlyList<DelivaryMethod>> GetDeliveryMethodsAsync(string buyerEmail);

      


    }
}
