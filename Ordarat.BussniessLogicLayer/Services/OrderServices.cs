using Ordarat.BussniessLogicLayer.Interfaces;
using Ordarat.DataAccessLayer.Entities;
using Ordarat.DataAccessLayer.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordarat.BussniessLogicLayer.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<DelivaryMethod> _delivaryMethodRepo;
        private readonly IGenericRepository<Order> _orderRepo;

        public OrderServices(IBasketRepository basketRepository , IGenericRepository<Product> ProductRepo , IGenericRepository<DelivaryMethod> DelivaryMethodRepo
             , IGenericRepository<Order> orderRepo
            )
        {
            _basketRepository = basketRepository;
            _productRepo = ProductRepo;
            _delivaryMethodRepo = DelivaryMethodRepo;
            _orderRepo = orderRepo;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address ShipToAddress)
        {
            var basket = await _basketRepository.GetCustomerBasket(basketId);

            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await _productRepo.GetAsync(item.Id);
                var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
                var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);
                orderItems.Add(orderItem);
            }

            var delivaryMethod = await _delivaryMethodRepo.GetAsync(deliveryMethodId);

            var subtotal = orderItems.Sum(item => item.Price * item.Quantitiy);

            var order = new Order(buyerEmail, ShipToAddress, delivaryMethod, subtotal , orderItems);

            await _orderRepo.Add(order);
            return order;
        }

        public Task<IReadOnlyList<DelivaryMethod>> GetDeliveryMethodsAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrdersbyIdForUser(int orderId, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
