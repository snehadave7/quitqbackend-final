using QuitQBackend.Data;
using QuitQBackend.Models;

namespace QuitQBackend.Repositories {
    public class ReviewService:IReviewService {
        private readonly QuitQContext _context;

        public ReviewService(QuitQContext context) {
            _context = context;
        }
        public int AddNewReview(Review review) {
            if (review != null) {
                _context.Reviews.Add(review);
                _context.SaveChanges();
                return review.Id;
            }
            return 0;
        }

        public string DeleteReview(int id) {
            if (id != null) {
                var Review = _context.Reviews.FirstOrDefault(x => x.Id == id);
                if (Review != null) {
                    _context.Reviews.Remove(Review);
                    _context.SaveChanges();
                    return "The given Review Id " + id + " is Removed";
                }
                else return "Something went wrong with deletion";
            }
            return null;
        }

        public List<Review> GetAllReviews() {
            var Review = _context.Reviews.ToList();
            if (Review.Count > 0) return Review;
            return null;
        }

        public Review GetReviewById(int id) {
            if (id != 0 || id != null) {
                var Review = _context.Reviews.FirstOrDefault(x => x.Id == id);
                if (Review != null) return Review;
                else return null;
            }
            return null;
        }

        public Review UpdateReview(Review review) {
            var existingReview = _context.Reviews.FirstOrDefault(x => x.Id == review.Id);
            if (existingReview != null) {
                existingReview.UserId = review.UserId;
                existingReview.ProductId = review.ProductId;
                existingReview.Rating = review.Rating;
                existingReview.Comment = review.Comment;
                existingReview.ReviewDate = review.ReviewDate;
                _context.Entry(existingReview).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();


            }
            return review;
        }
    }
}