using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuitQBackend.Models;
using QuitQBackend.Repositories;

namespace QuitQBackend.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase {
        private IOrderItemsService _service;
        public OrderItemsController(IOrderItemsService service) {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllOrderItem() {
            List<OrderItem> OrderItem = _service.GetAllOrderItems();

            return Ok(OrderItem);
        }
        [HttpGet("{id}")]
        public IActionResult GetOrderItemById(int id) {
            OrderItem OrderItem = _service.GetOrderItemById(id);

            if (OrderItem == null) {
                return NotFound();
            }
            return Ok(OrderItem);
        }
        [HttpPost]
        public IActionResult Post(OrderItem orderItem) {
            int result = _service.AddNewOrderItem(orderItem);


            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, OrderItem orderItem) {
            if (id != orderItem.Id) return BadRequest();
            OrderItem result = _service.UpdateOrderItem(orderItem);
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(int id) {
            string result = _service.DeleteOrderItem(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
