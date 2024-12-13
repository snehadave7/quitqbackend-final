using CaseStudyQuitQ.Models;

namespace CaseStudyQuitQ.Repository {
    public interface ICartItemService {
        Task<List<CartItem>> GetAllCartItems();
        Task<CartItem> GetCartItemById(int id);
        Task<List<Object>> GetCartItemByCartId(int cartId);
        Task<int> AddNewCartItem(CartItem CartItem);
        Task<CartItem> UpdateCartItem(CartItem CartItem);
        Task<string> DeleteCartItem(int id);
    }
}
