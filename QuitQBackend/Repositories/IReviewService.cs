using QuitQBackend.Models;

namespace QuitQBackend.Repositories {
    public interface IReviewService {
        List<Review> GetAllReviews();
        Review GetReviewById(int id);
        int AddNewReview(Review Review);
        Review UpdateReview(Review Review);
        string DeleteReview(int id);
    }
}
