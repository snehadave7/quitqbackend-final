using QuitQBackend.Models;

namespace QuitQBackend.Repositories {
    public interface ICartService {
        List<Cart> GetAllCarts();
        Cart GetCartById(int id);
        int AddNewCart(Cart Cart);
        Cart UpdateCart(Cart Cart);
        string DeleteCart(int id);

    }
}
