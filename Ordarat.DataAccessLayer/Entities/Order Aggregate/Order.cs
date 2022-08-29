using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordarat.DataAccessLayer.Entities.Order_Aggregate
{
    public class Order:BaseEntity
    {
        public Order()
        {

        }

        public Order(string buyerEmail, Address shipToAddress, DelivaryMethod delivaryMethod, decimal subtotal, List<OrderItem> items)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            DelivaryMethod = delivaryMethod;
            Subtotal = subtotal;
            Items = items;
        }

        public string BuyerEmail { get; set; }
        public Address ShipToAddress { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public DelivaryMethod DelivaryMethod { get; set; }

        public string PaymentIntentId { get; set; }
        public decimal Subtotal { get; set; }
        public List<OrderItem> Items { get; set; }

        public decimal GetTotal() //DTO
            => Subtotal + DelivaryMethod.Cost;
    }
}
