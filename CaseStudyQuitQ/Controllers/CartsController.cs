using CaseStudyQuitQ.Exceptions;
using CaseStudyQuitQ.Models;
using CaseStudyQuitQ.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudyQuitQ.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase {
        private ICartService _service;
        public CartsController(ICartService service) {
            _service = service;
        }

        [Authorize(Roles = "Seller")]
        [HttpGet]
        public async Task<IActionResult> GetAllCart() {
            try {
                List<Cart> cart = await _service.GetAllCarts();
                if (cart == null) throw new NullObjectException("No user was found");
                return Ok(cart);
            }
            catch (NullObjectException e) {
                return NotFound(e.Message);
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartById(int id) {
            try {
                if (id == 0) throw new InvalidIdException("Id cannot be 0");
                Cart cart = await _service.GetCartById(id);

                if (cart == null) throw new NullObjectException("No cart was found");
                return Ok(cart);
            }
            catch (InvalidIdException e) {
                return BadRequest(e.Message);
            }
            catch (NullObjectException e) {
                return NotFound(e.Message);
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> Post(Cart cart) {
            int result = await _service.AddNewCart(cart);


            return Ok(result);
        }

        [Authorize(Roles = "Customer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Cart cart) {
            try {
                if (id == 0) throw new InvalidIdException("Id cannot be 0");
                if (id != cart.Id) throw new InvalidIdException("Id should be same as cartId");
                Cart result = await _service.UpdateCart(cart);
                if (result == null) throw new NullObjectException("No cart was found to update");
                return Ok(result);
            }
            catch (InvalidIdException e) {
                return BadRequest(e.Message);
            }
            catch (NullObjectException e) {
                return NotFound(e.Message);
            }
        }


        [Authorize(Roles = "Customer")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id) {
            try {
                if (id == 0) throw new InvalidIdException("Id cannot be 0");
                string result = await _service.DeleteCart(id);
                if (result == null) throw new NullObjectException("No cart was found to delete");
                return Ok(result);
            }
            catch (InvalidIdException e) {
                return BadRequest(e.Message);
            }
            catch (NullObjectException e) {
                return NotFound(e.Message);
            }
        }
    }
}
