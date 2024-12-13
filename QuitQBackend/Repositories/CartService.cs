using QuitQBackend.Data;
using QuitQBackend.Models;

namespace QuitQBackend.Repositories {
    public class CartService : ICartService {
        private readonly QuitQContext _context;

        public CartService(QuitQContext context) {
            _context = context;
        }

        public int AddNewCart(Cart cart) {
            if (cart != null) {
                _context.Carts.Add(cart);
                _context.SaveChanges();
                return cart.Id;
            }
            return 0;
        }

        public string DeleteCart(int id) {
            if (id != null) {
                var cart = _context.Carts.FirstOrDefault(x => x.Id == id);
                if (cart != null) {
                    _context.Carts.Remove(cart);
                    _context.SaveChanges();
                    return "The given Cart Id " + id + " is Removed";
                }
                else return "Something went wrong with deletion";
            }
            return null;        
        }

        public List<Cart> GetAllCarts() {
            var carts = _context.Carts.ToList();
            if (carts.Count > 0) return carts;
            return null;
        }

        public Cart GetCartById(int id) {
            if (id != 0 || id != null) {
                var cart = _context.Carts.FirstOrDefault(x => x.Id == id);
                if (cart != null) return cart;
                else return null;
            }
            return null;
        }

        public Cart UpdateCart(Cart cart) {
            var existingCart = _context.Carts.FirstOrDefault(x => x.Id == cart.Id);
            if (existingCart != null) {
                existingCart.UserId = cart.UserId;
                _context.Entry(existingCart).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();


            }
            return cart;
        }
    }
}
