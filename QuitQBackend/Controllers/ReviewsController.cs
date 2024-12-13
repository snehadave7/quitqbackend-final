using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuitQBackend.Models;
using QuitQBackend.Repositories;

namespace QuitQBackend.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase {
        private IReviewService _service;
        public ReviewsController(IReviewService service) {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllReview() {
            List<Review> Review = _service.GetAllReviews();

            return Ok(Review);
        }
        [HttpGet("{id}")]
        public IActionResult GetReviewById(int id) {
            Review Review = _service.GetReviewById(id);

            if (Review == null) {
                return NotFound();
            }
            return Ok(Review);
        }
        [HttpPost]
        public IActionResult Post(Review review) {
            int result = _service.AddNewReview(review);


            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Review review) {
            if (id != review.Id) return BadRequest();
            Review result = _service.UpdateReview(review);
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(int id) {
            string result = _service.DeleteReview(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
