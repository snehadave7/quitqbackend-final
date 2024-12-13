using CaseStudyQuitQ.Exceptions;
using CaseStudyQuitQ.Models;
using CaseStudyQuitQ.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudyQuitQ.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase {
        private IProductService _service;
        public ProductsController(IProductService service) {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllProduct() {
            try {
                List<Product> product = await _service.GetAllProducts();
                if (product == null) throw new NullObjectException("No product was found");
                return Ok(product);
            }
            catch (NullObjectException e) {
                return NotFound(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("{sellerId}")]
        public async Task<IActionResult> GetProductBySellerId(int sellerId) {
            try {
                if (sellerId == 0) throw new InvalidIdException("Id cannot be 0");
                var product = await _service.GetProductBySellerId(sellerId);
                if (product == null) throw new NullObjectException("No product was found with id: " + sellerId);
                
                return Ok(product);
            }
            catch (InvalidIdException e) {
                return BadRequest(e.Message);
            }
            catch (NullObjectException e) {
                return NotFound(e.Message);
            }
        }


        [Authorize(Roles = "Seller")]
        [HttpPost]
        public async Task<IActionResult> Post(Product product) {

            int result = await _service.AddNewProduct(product);

            Console.WriteLine(product);
            return Ok(result);
        }

        [Authorize(Roles = "Seller")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product product) {
            try {
                if (id == 0) throw new InvalidIdException("Id cannot be 0");
                if (id != product.Id) throw new InvalidIdException("Id should be same as productId");
                Product result = await _service.UpdateProduct(product);
                if (result == null) throw new NullObjectException("No product was found to update");

                return Ok(result);
            }
            catch (InvalidIdException e) {
                return BadRequest(e.Message);
            }
            catch (NullObjectException e) {
                return NotFound(e.Message);
            }
        }

        [Authorize(Roles = "Seller")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id) {
            try {
                if (id == 0) throw new InvalidIdException("Id cannot be 0");
                string result = await _service.DeleteProduct(id);
                if (result == null) throw new NullObjectException("No product was found to delete");
                return Ok(result);
            }
            catch (InvalidIdException e) {
                return BadRequest(e.Message);
            }
            catch (NullObjectException e) {
                return NotFound(e.Message);
            }
        }


        [AllowAnonymous]
        [HttpGet("by-ProductName/{productName}")]
        public async Task<IActionResult> GetByProductByName(string productName) {
            try {
                var result = await _service.SearchByProductName(productName);
                if (result == null) throw new NullObjectException("No product was found with productName: " + productName);
                return Ok(result);
            }
            catch (NullObjectException e) {
                return NotFound(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("by-subCat/{subCatName}")]
        public async Task<IActionResult> GetByProductBySubCat(string subCatName) {
            try {
                var subCategoryList = subCatName.Split(',').ToList();
                var result = await _service.SearchBySubCategory(subCategoryList);
                if (result == null || !result.Any()) throw new NullObjectException("No product was found with subCategory: " + subCatName);
                return Ok(result);
            }
            catch (NullObjectException e) {
                return NotFound(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("by-cat/{catName}")]
        public async Task<IActionResult> GetByProductByCat(string catName) {
            try {
                var result = await _service.SearchByProductCategory(catName);
                if (result == null) throw new NullObjectException("No product was found with category: " + catName);
                return Ok(result);
            }
            catch (NullObjectException e) {
                return NotFound(e.Message);
            }
        }


        

    }
}
