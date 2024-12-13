using CaseStudyQuitQ.Exceptions;
using CaseStudyQuitQ.Models;
using CaseStudyQuitQ.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudyQuitQ.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryAddressesController : ControllerBase {
        private IDeliveryAddressService _service;
        public DeliveryAddressesController(IDeliveryAddressService service) {
            _service = service;
        }

        [Authorize(Roles = "Customer")]
        [HttpGet]
        public async Task<IActionResult> GetAllDeliveryAddress([FromQuery] int userId) {
            try {
                List<DeliveryAddress> deliveryAddress = await _service.GetAllDeliveryAddresss(userId);
                if (deliveryAddress == null) throw new NullObjectException("Delivery Address not found");
                return Ok(deliveryAddress);
            }
            
            catch (NullObjectException e) {
                return NotFound(e.Message);
            }


        }

        
        [Authorize(Roles = "Customer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeliveryAddressById(int id) {
            try {
                if (id == 0) throw new InvalidIdException("Id cannot be 0");
                DeliveryAddress deliveryAddress = await _service.GetDeliveryAddressById(id);

                if (deliveryAddress == null) throw new NullObjectException("No address was found with id: " + id);

                return Ok(deliveryAddress);
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
        public async Task<IActionResult> Post(DeliveryAddress deliveryAddress) {
            
            int result =await _service.AddNewDeliveryAddress(deliveryAddress);


            return Ok(result);
        }


        [Authorize(Roles = "Customer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DeliveryAddress deliveryAddress) {
            try {
                if (id == 0) throw new InvalidIdException("Id cannot be 0");
                if (id != deliveryAddress.Id) throw new InvalidIdException("Id should be same as AddressId");
                DeliveryAddress result = await _service.UpdateDeliveryAddress(deliveryAddress);
                if (result == null) throw new NullObjectException("No address was found to update");
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
                string result = await _service.DeleteDeliveryAddress(id);
                if (result == null) throw new NullObjectException("No DeliveryAddress was found to delete");
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
