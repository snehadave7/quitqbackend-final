using CaseStudyQuitQ.Models;

namespace CaseStudyQuitQ.Repository {
    public interface ICartService {
        Task<List<Cart>> GetAllCarts();
        Task<Cart> GetCartById(int id);
        Task<int> AddNewCart(Cart Cart);
        Task<Cart> UpdateCart(Cart Cart);
        Task<string> DeleteCart(int id);

    }
}
