using CaseStudyQuitQ.Data;
using CaseStudyQuitQ.Exceptions;
using CaseStudyQuitQ.Models;
using CaseStudyQuitQ.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudyQuitQ.Controllers {
    [Route("api/[controller]")]
    [ApiController] 
    public class CartItemsController : ControllerBase {
        private ICartItemService _service;
        

        public CartItemsController(ICartItemService service) {
            _service = service;
        }


        //[Authorize(Roles = "Customer")]
        //[HttpGet]
        //public async Task<IActionResult> GetAllCartItem() {
        //    try {
        //        List<CartItem> cartItem = await _service.GetAllCartItems();
        //        if (cartItem == null) throw new NullObjectException("No cartItem was found");
        //        return Ok(cartItem);
        //    }
            
        //    catch (NullObjectException e) {
        //        return NotFound(e.Message);
        //    }
        //}

        [Authorize(Roles = "Customer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartItemById(int id) {
            try {
                if (id == 0) throw new InvalidIdException("Id cannot be 0");
                CartItem cartItem = await _service.GetCartItemById(id);
                if (cartItem == null) throw new NullObjectException("No cartItem was found with id: " + id);
                return Ok(cartItem);
            }
            catch (InvalidIdException e) {
                return BadRequest(e.Message);
            }
            catch (NullObjectException e) {
                return NotFound(e.Message);
            }

        }
        [Authorize(Roles = "Customer")]
        [HttpGet]
        public async Task<IActionResult> GetCartItemByCartId([FromQuery]int cartId) {
            try {
                if (cartId == 0) throw new InvalidIdException("Id cannot be 0");
                var cartItem = await _service.GetCartItemByCartId(cartId);
                if (cartItem == null) throw new NullObjectException("No cartItem was found with id: " + cartId);
                return Ok(cartItem);
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
        public async Task<IActionResult> Post(CartItem cartItem) {
            int result = await _service.AddNewCartItem(cartItem);
            if (result == -1) return BadRequest("Not enough stock");
            else if (result == 0) return BadRequest("Failed to add item");
            
            var addedItem=_service.GetCartItemById(result);
            return Ok(addedItem);
        }

        [Authorize(Roles = "Customer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]CartItem cartItem) {
            try {
                if (id == 0) throw new InvalidIdException("Id cannot be 0");
                if (id != cartItem.Id) throw new InvalidIdException("Id should be same as cartItemId");
                CartItem result = await _service.UpdateCartItem(cartItem);
                if (result == null) throw new NullObjectException("No cartItem was found to update");
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
                string result = await _service.DeleteCartItem(id);
                if (result == null) throw new NullObjectException("No carItem was found to delete");
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
