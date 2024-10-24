using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Snai3i_BLL.DTO.OrderDto;
using Snai3i_BLL.DTO.ReviewDto;
using Snai3i_BLL.Manager.OrdersManager;

namespace Snai3i_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager _orderManager;

        public OrderController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        // POST: api/order
        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderAddDTO orderAddDTO)
        {
            await _orderManager.AddOrderAsync(orderAddDTO);
            return Ok("Order created successfully");
        }
        //public async Task<IActionResult> GetAllOrders()
        //{
        //    try
        //    {
        //        var Orders = await _orderManager.GetAllOrdersAsync();
        //        return Ok(Orders);

        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound(ex.InnerException.Message);
        //    }
        //}

        // GET: api/order/worker/{workerId}
        [HttpGet("{workerId}")]
        public async Task<IActionResult> GetOrdersForWorker(string workerId)
        {
            var orders = await _orderManager.GetOrdersForWorkerAsync(workerId);
            return Ok(orders);
        }

        //[HttpGet("user/{userId}")]
        //public async Task<IActionResult> GetOrdersForUser(string userId)
        //{
        //    var orders = await _orderManager.GetOrdersForUserAsync(userId);
        //    return Ok(orders);
        //}

        //[HttpGet]
        //[Route("{Id}")]
        //public async Task<IActionResult> GetOrderById(int Id)
        //{
        //    try
        //    {
        //        var Order = await _orderManager.GetOrderByIdAsync(Id);
        //        return Ok(Order);

        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound(ex.InnerException.Message);
        //    }

        //}
        //[HttpPut]
        //[Route("{Id}")]
        //public async Task<IActionResult> Edit(int Id, OrderUpdateDTO orderUpdateDTO)
        //{
        //    try
        //    {
        //        if (Id != orderUpdateDTO.Id)
        //        {
        //            return BadRequest();
        //        }

        //        await _orderManager.EidetOrderAsync(orderUpdateDTO);
        //        return NoContent();

        //    }
        //    catch (Exception ex)
        //    {
        //        return Content($"cant alter your order as + {ex.InnerException.Message}");
        //    }

        //}

        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> DeleteOrder(int Id)
        {
            try
            {
                await _orderManager.DeleteOrderAsync(Id);
                return Content("your order is deleted successfuly!");

            }
            catch (Exception ex)
            {
                return NotFound(ex.InnerException.Message);
            }


        }


    }
}
