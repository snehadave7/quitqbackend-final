using CaseStudyQuitQ.Exceptions;
using CaseStudyQuitQ.Models;
using CaseStudyQuitQ.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudyQuitQ.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase {
        private IReviewService _service;
        public ReviewsController(IReviewService service) {
            _service = service;
        }


        [Authorize(Roles = "Seller")]
        [HttpGet("{SellerId})")]
        public async Task<IActionResult> GetAllReview(int SellerId) {
            try {
                if (SellerId == 0) throw new InvalidIdException("Id cannot be 0");
                List<Review> Review = await _service.GetAllReviews(SellerId);
                if (Review == null) throw new NullObjectException("No Review was found");
                return Ok(Review);
            }
            catch (NullObjectException e) {
                return NotFound(e.Message);
            }
        }
        
        [Authorize(Roles = "Customer")]
        [HttpGet("{ProductId}")]
        public async Task<IActionResult> GetReviewById(int ProductId) {
            try {
                if (ProductId == 0) throw new InvalidIdException("Id cannot be 0");
                List<Review> Review = await _service.GetReviewByProductId(ProductId);
                if (Review == null) throw new NullObjectException("No Review was found with id: " + ProductId);
               
                return Ok(Review);
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
        public async Task<IActionResult> Post(Review review) {
            int result = await _service.AddNewReview(review);


            return Ok(result);
        }

        [Authorize(Roles = "Customer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Review review) {
            try {
                if (id == 0) throw new InvalidIdException("Id cannot be 0");
                if (id != review.Id) throw new InvalidIdException("Id should be same as reviewId");
                Review result = await _service.UpdateReview(review);
                if (result == null) throw new NullObjectException("No review was found to update");
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
                string result = await _service.DeleteReview(id);
                if (result == null) throw new NullObjectException("No review was found to delete");
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
