using CaseStudyQuitQ.Exceptions;
using CaseStudyQuitQ.Models;
using CaseStudyQuitQ.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudyQuitQ.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriesController : ControllerBase {
        private ISubCategoryService _service;
        public SubCategoriesController(ISubCategoryService service) {
            _service = service;
        }

        //[AllowAnonymous]
        //[HttpGet]
        //public async Task<IActionResult> GetAllSubCategory() {
        //    try {
        //        List<SubCategory> subCategory = await _service.GetAllSubCategorys();
        //        if (subCategory == null) throw new NullObjectException("No subCategory was found");
        //        return Ok(subCategory);
        //    }
        //    catch (NullObjectException e) {
        //        return NotFound(e.Message);
        //    }
        //}



        //[AllowAnonymous]
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetSubCategoryById(int id) {
        //    try {
        //        if (id == 0) throw new InvalidIdException("Id cannot be 0");
        //        SubCategory subCategory = await _service.GetSubCategoryById(id);
        //        if (subCategory == null) throw new NullObjectException("No subCategory was found with id: " + id);
        //        if (subCategory == null) {
        //            return NotFound();
        //        }
        //        return Ok(subCategory);
        //    }
        //    catch (InvalidIdException e) {
        //        return BadRequest(e.Message);
        //    }
        //    catch (NullObjectException e) {
        //        return NotFound(e.Message);
        //    }
        //}

        [AllowAnonymous]
        [HttpGet()]
        public async Task<IActionResult> GetSubCategoryByCategoryId([FromQuery] int catId) {

            var subCategory = await _service.GetSubCategoryByCatId(catId);
            if (subCategory != null) return Ok(subCategory);
            else return NotFound();
        }




        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(SubCategory subCategory) {
            int result = await _service.AddNewSubCategory(subCategory);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SubCategory subCategory) {
            try {
                if (id == 0) throw new InvalidIdException("Id cannot be 0");
                if (id != subCategory.Id) throw new InvalidIdException("Id should be same as SubCatId");
                SubCategory result = await _service.UpdateSubCategory(subCategory);
                if (result == null) throw new NullObjectException("No SubCat was found to update");

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
                string result = await _service.DeleteSubCategory(id);
                if (result == null) throw new NullObjectException("No subCat was found to delete");
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
