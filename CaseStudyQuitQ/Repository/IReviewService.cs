using CaseStudyQuitQ.Models;

namespace CaseStudyQuitQ.Repository {
    public interface IReviewService {
        Task<List<Review>> GetAllReviews(int id);
        Task<List<Review>> GetReviewByProductId(int id);
        Task<int> AddNewReview(Review Review);
        Task<Review> UpdateReview(Review Review);
        Task<string> DeleteReview(int id);
    }
}
