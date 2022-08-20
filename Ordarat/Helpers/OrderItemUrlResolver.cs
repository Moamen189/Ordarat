using AutoMapper;
using Microsoft.Extensions.Configuration;
using Ordarat.DataAccessLayer.Entities.Order_Aggregate;
using Ordarat.Dtos;

namespace Ordarat.Helpers
{
    public class OrderItemUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration configration;

        public OrderItemUrlResolver(IConfiguration configration)
        {
            this.configration = configration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ItemOrder.PictureUrl))
                return $"{configration["ApiURl"]}{source.ItemOrder.PictureUrl}";
            return null;
        }
    }
}
