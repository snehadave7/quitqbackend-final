using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuitQBackend.Models;
using QuitQBackend.Repositories;

namespace QuitQBackend.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase {
        private IProductService _service;
        public ProductsController(IProductService service) {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllProduct() {
            List<Product> product = _service.GetAllProducts();

            return Ok(product);
        }
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id) {
            Product product = _service.GetProductById(id);

            if (product == null) {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public IActionResult Post(Product product) {
            int result = _service.AddNewProduct(product);


            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Product product) {
            if (id != product.Id) return BadRequest();
            Product result = _service.UpdateProduct(product);
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(int id) {
            string result = _service.DeleteProduct(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
