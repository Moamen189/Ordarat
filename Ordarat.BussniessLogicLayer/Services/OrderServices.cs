using Ordarat.BussniessLogicLayer.Interfaces;
using Ordarat.BussniessLogicLayer.Specification.Order_Specification;
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
        private readonly IUnitOfWork unitOfWork;
        //private readonly IGenericRepository<Product> _productRepo;
        //private readonly IGenericRepository<DelivaryMethod> _delivaryMethodRepo;
        //private readonly IGenericRepository<Order> _orderRepo;

        public OrderServices(IBasketRepository basketRepository  //IGenericRepository<Product> ProductRepo , IGenericRepository<DelivaryMethod> DelivaryMethodRepo
        //     , IGenericRepository<Order> orderRepo
                , IUnitOfWork unitOfWork
            )
        {
            _basketRepository = basketRepository;
            this.unitOfWork = unitOfWork;
            //_productRepo = ProductRepo;
            //_delivaryMethodRepo = DelivaryMethodRepo;
            //_orderRepo = orderRepo;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address ShipToAddress)
        {
            var basket = await _basketRepository.GetCustomerBasket(basketId);

            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await unitOfWork.Repository<Product>().GetAsync(item.Id);
                var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
                var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);
                orderItems.Add(orderItem);
            }

            var delivaryMethod = await unitOfWork.Repository<DelivaryMethod>().GetAsync(deliveryMethodId);

            var subtotal = orderItems.Sum(item => item.Price * item.Quantitiy);

            var order = new Order(buyerEmail, ShipToAddress, delivaryMethod, subtotal , orderItems);

            await unitOfWork.Repository<Order>().Add(order);
            int result = await unitOfWork.Complete();
            if(result <=0)
                return null;
            return order;
        }

        public async Task<IReadOnlyList<DelivaryMethod>> GetDeliveryMethodsAsync()
        {
           return await unitOfWork.Repository<DelivaryMethod>().GetAllAsync();
        }

        public async Task<Order> GetOrdersbyIdForUser(int orderId, string buyerEmail)
        {
            var spec = new OrderWithItemAndDeleveryMethodSpecificatioin(orderId, buyerEmail);
            var order = await unitOfWork.Repository<Order>().GetWithSpecAsync(spec);
            return order;
            
            
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrderWithItemAndDeleveryMethodSpecificatioin(buyerEmail);
            var orders = await unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
            return orders;
        }
    }
}
