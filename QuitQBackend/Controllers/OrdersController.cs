using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuitQBackend.Models;
using QuitQBackend.Repositories;

namespace QuitQBackend.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase {
        private IOrderService _service;
        public OrdersController(IOrderService service) {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllOrder() {
            List<Order> order = _service.GetAllOrders();

            return Ok(order);
        }
        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id) {
            Order Order = _service.GetOrderById(id);

            if (Order == null) {
                return NotFound();
            }
            return Ok(Order);
        }
        [HttpPost]
        public IActionResult Post(Order order) {
            int result = _service.AddNewOrder(order);


            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Order order) {
            if (id != order.Id) return BadRequest();
            Order result = _service.UpdateOrder(order);
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(int id) {
            string result = _service.DeleteOrder(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
