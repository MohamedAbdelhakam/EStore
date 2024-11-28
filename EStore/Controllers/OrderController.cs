using EStore.Core.Constants;
using EStore.Services.AdminServices.RepositoryServices.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using UserServices.Orders;

namespace EStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderAdminService _orderAdminService;
        private readonly IOrderService _orderService;

        public OrderController(IOrderAdminService orderAdminService, IOrderService orderService) 
        {
            this._orderAdminService = orderAdminService;
            this._orderService = orderService;
        }
        private IActionResult ValidateRequest(out int parsedCartId)
        {
            if (!ModelState.IsValid)
            {
                parsedCartId = 0;
                return BadRequest(ModelState);
            }

            var cartId = User.FindFirstValue("CartId");
            if (string.IsNullOrEmpty(cartId))
            {
                parsedCartId = 0;
                return Unauthorized("Invalid or missing cart information");
            }

            if (!int.TryParse(cartId, out parsedCartId))
                return BadRequest("Invalid cart ID format");

            return null;
        }
        [HttpPost("MakeOrder")]
        [Authorize( "Customer")]
        public async Task<IActionResult> MakOrder(string ShippingAddress) 
        {
            var validation = ValidateRequest(out int parsedCartId);
            if (validation != null)
                return validation;
            var result = await _orderService.MakeOrder(parsedCartId,ShippingAddress);
            return result.IsSucceed ? Ok(result) : BadRequest(result);
        }
        [HttpGet("GetOrder")]

        [Authorize("Customer")]
        public async Task<IActionResult> GetOrders() 
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _orderService.GetOrders(UserId);
            return result.IsSucceed ? Ok(result) : BadRequest(result);
        }
        [HttpPut("ChangeOrderStatus")]
        [Authorize("Administrator")]
        public async Task<IActionResult> ChangeOrderStatus(int OrderId,OrderStatus orderStatus)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _orderAdminService.ChangeOrderStatus(OrderId,orderStatus);
            return result.IsSucceed ? Ok(result) : BadRequest(result);
        }
    }
}
