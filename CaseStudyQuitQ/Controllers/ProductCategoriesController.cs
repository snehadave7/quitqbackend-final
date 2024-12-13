using CaseStudyQuitQ.Exceptions;
using CaseStudyQuitQ.Models;
using CaseStudyQuitQ.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudyQuitQ.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoriesController : ControllerBase {
        private IProductCategoryService _service;
        public ProductCategoriesController(IProductCategoryService service) {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllProductCategory() {
            try {
                List<ProductCategory> productCategory = await _service.GetAllProductCategory();
                if (productCategory == null) throw new NullObjectException("No productCategory was found");

                return Ok(productCategory);
            }
            catch (NullObjectException e) {
                return NotFound(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductCategoryById(int id) {
            try {
                if (id == 0) throw new InvalidIdException("Id cannot be 0");
                ProductCategory productCategory = await _service.GetProductCategoryById(id);
                if (productCategory == null) throw new NullObjectException("No productCategory was found with id: " + id);

                return Ok(productCategory);
            }
            catch (InvalidIdException e) {
                return BadRequest(e.Message);
            }
            catch (NullObjectException e) {
                return NotFound(e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(ProductCategory productCategory) {
            int result = await _service.AddNewProductCategory(productCategory);


            return Ok(result);
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductCategory productCategory) {
            try {
                if (id == 0) throw new InvalidIdException("Id cannot be 0");
                if (id != productCategory.Id) throw new InvalidIdException("Id should be same as productCategoryId");
                ProductCategory result = await _service.UpdateProductCategory(productCategory);
                if (result == null) throw new NullObjectException("No ProductCategory was found to update");
                return Ok(result);
            }
            catch (InvalidIdException e) {
                return BadRequest(e.Message);
            }
            catch (NullObjectException e) {
                return NotFound(e.Message);
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id) {
            try {
                if (id == 0) throw new InvalidIdException("Id cannot be 0");
                string result = await _service.DeleteProductCategory(id);
                if (result == null) throw new NullObjectException("No ProductCategory was found to delete");
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
