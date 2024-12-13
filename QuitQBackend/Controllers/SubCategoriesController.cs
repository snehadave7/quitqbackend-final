using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuitQBackend.Models;
using QuitQBackend.Repositories;

namespace QuitQBackend.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriesController : ControllerBase {
        private ISubCategoryService _service;
        public SubCategoriesController(ISubCategoryService service) {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllSubCategory() {
            List<SubCategory> subCategory = _service.GetAllSubCategorys();

            return Ok(subCategory);
        }
        [HttpGet("{id}")]
        public IActionResult GetSubCategoryById(int id) {
            SubCategory subCategory = _service.GetSubCategoryById(id);

            if (subCategory == null) {
                return NotFound();
            }
            return Ok(subCategory);
        }
        [HttpPost]
        public IActionResult Post(SubCategory subCategory) {
            int result = _service.AddNewSubCategory(subCategory);


            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, SubCategory subCategory) {
            if (id != subCategory.Id) return BadRequest();
            SubCategory result = _service.UpdateSubCategory(subCategory);
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(int id) {
            string result = _service.DeleteSubCategory(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

    }
}
