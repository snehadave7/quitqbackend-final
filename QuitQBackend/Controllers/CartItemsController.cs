using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuitQBackend.Models;
using QuitQBackend.Repositories;

namespace QuitQBackend.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase {
        private ICartItemService _service;
        public CartItemsController(ICartItemService service) {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllCartItem() {
            List<CartItem> cartItem = _service.GetAllCartItems();

            return Ok(cartItem);
        }
        [HttpGet("{id}")]
        public IActionResult GetCartItemById(int id) {
            CartItem cartItem = _service.GetCartItemById(id);

            if (cartItem == null) {
                return NotFound();
            }
            return Ok(cartItem);
        }
        [HttpPost]
        public IActionResult Post(CartItem cartItem) {
            int result = _service.AddNewCartItem(cartItem);


            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CartItem cartItem) {
            if (id != cartItem.Id) return BadRequest();
            CartItem result = _service.UpdateCartItem(cartItem);
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(int id) {
            string result = _service.DeleteCartItem(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}

