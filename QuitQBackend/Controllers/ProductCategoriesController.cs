using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuitQBackend.Models;
using QuitQBackend.Repositories;

namespace QuitQBackend.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoriesController : ControllerBase {
        private IProductCategoryService _service;
        public ProductCategoriesController(IProductCategoryService service) {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllProductCategory() {
            List<ProductCategory> productCategory = _service.GetAllProductCategory();

            return Ok(productCategory);
        }
        [HttpGet("{id}")]
        public IActionResult GetProductCategoryById(int id) {
            ProductCategory productCategory = _service.GetProductCategoryById(id);

            if (productCategory == null) {
                return NotFound();
            }
            return Ok(productCategory);
        }
        [HttpPost]
        public IActionResult Post(ProductCategory productCategory) {
            int result = _service.AddNewProductCategory(productCategory);


            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ProductCategory productCategory) {
            if (id != productCategory.Id) return BadRequest();
            ProductCategory result = _service.UpdateProductCategory(productCategory);
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(int id) {
            string result = _service.DeleteProductCategory(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

    }
}
