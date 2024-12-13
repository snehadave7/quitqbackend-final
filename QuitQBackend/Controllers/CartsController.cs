using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuitQBackend.Models;
using QuitQBackend.Repositories;

namespace QuitQBackend.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase {
        private ICartService _service;
        public CartsController(ICartService service) {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllCart() {
            List<Cart> Cart = _service.GetAllCarts();

            return Ok(Cart);
        }
        [HttpGet("{id}")]
        public IActionResult GetCartById(int id) {
            Cart cart = _service.GetCartById(id);

            if (cart == null) {
                return NotFound();
            }
            return Ok(cart);
        }
        [HttpPost]
        public IActionResult Post(Cart cart) {
            int result = _service.AddNewCart(cart);


            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Cart cart) {
            if (id != cart.Id) return BadRequest();
            Cart result = _service.UpdateCart(cart);
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(int id) {
            string result = _service.DeleteCart(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
