using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuitQBackend.Models;
using QuitQBackend.Repositories;

namespace QuitQBackend.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryAddressesController : ControllerBase {
        private IDeliveryAddressService _service;
        public DeliveryAddressesController(IDeliveryAddressService service) {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllDeliveryAddress() {
            List<DeliveryAddress> DeliveryAddress = _service.GetAllDeliveryAddresss();

            return Ok(DeliveryAddress);
        }
        [HttpGet("{id}")]
        public IActionResult GetDeliveryAddressById(int id) {
            DeliveryAddress DeliveryAddress = _service.GetDeliveryAddressById(id);

            if (DeliveryAddress == null) {
                return NotFound();
            }
            return Ok(DeliveryAddress);
        }
        [HttpPost]
        public IActionResult Post(DeliveryAddress deliveryAddress) {
            int result = _service.AddNewDeliveryAddress(deliveryAddress);


            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, DeliveryAddress deliveryAddress) {
            if (id != deliveryAddress.Id) return BadRequest();
            DeliveryAddress result = _service.UpdateDeliveryAddress(deliveryAddress);
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(int id) {
            string result = _service.DeleteDeliveryAddress(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
