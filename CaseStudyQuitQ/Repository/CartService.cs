using CaseStudyQuitQ.Data;
using CaseStudyQuitQ.Models;

namespace CaseStudyQuitQ.Repository {
    public class CartService : ICartService {
        private readonly QuitQEcomContext _context;

        public CartService(QuitQEcomContext context) {
            _context = context;
        }

        public async Task<int> AddNewCart(Cart cart) {
            if (cart != null) {
                _context.Carts.Add(cart);
                _context.SaveChanges();
                return cart.Id;
            }
            return 0;
        }

        public async Task<string> DeleteCart(int id) {
            if (id != null) {
                var cart = _context.Carts.FirstOrDefault(x => x.Id == id);
                if (cart != null) {
                    _context.Carts.Remove(cart);
                    _context.SaveChanges();
                    return "The given Cart Id " + id + " is Removed";
                }
                else return null;
            }
            return null;        
        }

        public async Task<List<Cart>> GetAllCarts() {
            var carts = _context.Carts.ToList();
            if (carts.Count > 0) return carts;
            return null;
        }

        public async Task<Cart> GetCartById(int id) {
            if (id != 0 || id != null) {
                var cart = _context.Carts.FirstOrDefault(x => x.UserId == id);
                if (cart != null) return cart;
                else return null;
            }
            return null;
        }

        public async Task<Cart> UpdateCart(Cart cart) {
            var existingCart = _context.Carts.FirstOrDefault(x => x.Id == cart.Id);
            if (existingCart != null) {
                existingCart.UserId = cart.UserId;
                _context.Entry(existingCart).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return cart;

            }
            return null;
        }
    }
}
