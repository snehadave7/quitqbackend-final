using CaseStudyQuitQ.Data;
using CaseStudyQuitQ.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseStudyQuitQ.Repository {
    public class ReviewService:IReviewService {
        private readonly QuitQEcomContext _context;

        public ReviewService(QuitQEcomContext context) {
            _context = context;
        }
        public async Task<int> AddNewReview(Review review) {
            if (review != null) {
                _context.Reviews.Add(review);
                _context.SaveChanges();
                return review.Id;
            }
            return 0;
        }

        public async Task<string> DeleteReview(int id) {
            if (id != null) {
                var Review = _context.Reviews.FirstOrDefault(x => x.Id == id);
                if (Review != null) {
                    _context.Reviews.Remove(Review);
                    _context.SaveChanges();
                    return "The given Review Id " + id + " is Removed";
                }
                else return null;
            }
            return null;
        }

        public async Task<List<Review>> GetAllReviews(int id) {
            var products = _context.Products.Where(p => p.SellerId == id).Select(p=>p.Id).ToList();
            if (id != 0 || id != null) {
                var reviews = _context.Reviews.Where(r => products.Contains(r.ProductId.Value)).ToList();
                if (reviews != null) return reviews;
                else return null;
            }
            return null;
        }

        public async Task<List<Review> >GetReviewByProductId(int id) {
            if (id != 0 || id != null) {
                var Review = await _context.Reviews.Include(x=>x.User).Where(x =>x.ProductId==id).ToListAsync();
                if (Review != null) return Review;
                else return null;
            }
            return null;
        }

        public async Task<Review> UpdateReview(Review review) {
            var existingReview = _context.Reviews.FirstOrDefault(x => x.Id == review.Id);
            if (existingReview != null) {
                existingReview.UserId = review.UserId;
                existingReview.ProductId = review.ProductId;
                existingReview.Rating = review.Rating;
                existingReview.Comment = review.Comment;
                existingReview.ReviewDate = review.ReviewDate;
                _context.Entry(existingReview).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();

                return review;
            }
            return null;
        }
    }
}