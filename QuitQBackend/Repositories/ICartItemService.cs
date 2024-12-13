using QuitQBackend.Models;

namespace QuitQBackend.Repositories {
    public interface ICartItemService {
        List<CartItem> GetAllCartItems();
        CartItem GetCartItemById(int id);
        int AddNewCartItem(CartItem CartItem);
        CartItem UpdateCartItem(CartItem CartItem);
        string DeleteCartItem(int id);
    }
}
