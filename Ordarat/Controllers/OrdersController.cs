using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordarat.BussniessLogicLayer.Interfaces;
using Ordarat.DataAccessLayer.Entities.Order_Aggregate;
using Ordarat.Dtos;
using Ordarat.Errors;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ordarat.Controllers
{
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderServices _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderServices orderService , IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        [HttpPost]

        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var orderAddress = _mapper.Map<AddressDto, Address>(orderDto.shipToAddress);
            var result = await _orderService.CreateOrderAsync(buyerEmail, orderDto.BasketId, orderDto.DeliveryMethod, orderAddress);
            if (result == null) 
                return BadRequest(new ApiResponse(400, "An Error Occured During Creating The Order"));
             
            return Ok(result);  
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>>GetOrdersForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetOrdersForUserAsync(buyerEmail);
            return Ok(_mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders));
        }

        [HttpGet("{id}")] // /{id}
        public async Task<ActionResult<OrderToReturnDto>> GetOrderForUser(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetOrdersbyIdForUser(id,buyerEmail);
            return Ok(_mapper.Map<Order, OrderToReturnDto>(orders));
        }

        [HttpGet("deliverymethods")] // /deliverymethods
        public async Task<ActionResult<IReadOnlyList<DelivaryMethod>>> GetDeliveryMethods()
        {
           
            return Ok(await _orderService.GetDeliveryMethodsAsync());
        }
    }
}
