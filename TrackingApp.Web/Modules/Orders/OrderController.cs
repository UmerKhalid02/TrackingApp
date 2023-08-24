using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackingApp.Application.DataTransferObjects.OrderDTO;
using TrackingApp.Application.Parameters;
using TrackingApp.Web.Modules.Common;

namespace TrackingApp.Web.Modules.Orders
{
    [Route("api/v1/orders")]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize(Roles = "AD")]
        [HttpGet]
        public async Task<IActionResult> GetAllActiveOrders([FromQuery] OrderPageParamter request)
        {
            return Ok(await _orderService.GetAllActiveOrders(request));
        }

        [Authorize(Roles = "AD")]
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetActiveOrderById(int orderId)
        {
            return Ok(await _orderService.GetActiveOrderById(orderId));
        }

        [Authorize(Roles = "AD")]
        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] AddOrderRequestDTO request)
        {
            return Ok(await _orderService.AddOrder(request));
        }

        [Authorize(Roles = "AD")]
        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder(int orderId, [FromBody] UpdateOrderRequestDTO request)
        {
            return Ok(await _orderService.UpdateOrder(orderId, request));
        }

        [Authorize(Roles = "AD")]
        [HttpPatch("{orderId}")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromQuery] string status)
        {
            return Ok(await _orderService.UpdateOrderStatus(orderId, status));
        }

        [Authorize(Roles = "AD")]
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            return Ok(await _orderService.DeleteOrder(orderId));
        }

    }
}
