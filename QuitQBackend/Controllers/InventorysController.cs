using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuitQBackend.Models;
using QuitQBackend.Repositories;

namespace QuitQBackend.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class InventorysController : ControllerBase {
        private IInventoryService _service;
        public InventorysController(IInventoryService service) {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllInventory() {
            List<Inventory> Inventory = _service.GetAllInventorys();

            return Ok(Inventory);
        }
        [HttpGet("{id}")]
        public IActionResult GetInventoryById(int id) {
            Inventory Inventory = _service.GetInventoryById(id);

            if (Inventory == null) {
                return NotFound();
            }
            return Ok(Inventory);
        }
        [HttpPost]
        public IActionResult Post(Inventory inventory) {
            int result = _service.AddNewInventory(inventory);


            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Inventory inventory) {
            if (id != inventory.Id) return BadRequest();
            Inventory result = _service.UpdateInventory(inventory);
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(int id) {
            string result = _service.DeleteInventory(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
