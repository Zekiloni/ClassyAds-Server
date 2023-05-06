using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAds.Interfaces;
using MyAds.Entities;

namespace MyAds.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IOrderService _orders;

        public OrderController(IOrderService orders, IConfiguration config)
        {
            _configuration = config;
            _orders = orders;
        }

        [HttpGet("/orders/{orderId}")]
        public async Task<IActionResult> GetClassifiedById(int orderId)
        {
            var order = await _orders.GetOrderById(orderId);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost("/orders/create")]
        public async Task<IActionResult> CreateOrder(int categoryId, string title, string shortDescription, string description, int amount)
        {

            var classified = new Order
            {
                CategoryId = categoryId,
                Title = title,
                ShortDescription = shortDescription,
                Description = description
            };


            if (classified == null)
            {
                return BadRequest();
            }

            try
            {
                await _orders.CreateOrder(classified);
            }
            catch (Exception errorCreating)
            {
                return BadRequest(errorCreating);
            }


            return Ok(classified);
        }
    }
}
