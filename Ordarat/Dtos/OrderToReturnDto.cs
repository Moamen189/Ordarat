using Ordarat.DataAccessLayer.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;

namespace Ordarat.Dtos
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public Address ShipToAddress { get; set; }
        public string DelivaryMethod { get; set; }
        public DateTimeOffset OrderDate { get; set; } 
        public string Status { get; set; } 
        public int PaymentIntentId { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DeliveryCost { get; set; }
        public decimal Total { get; set; }


        public List<OrderItemDto> Items { get; set; }
    }
}
