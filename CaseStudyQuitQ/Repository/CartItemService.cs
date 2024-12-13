using CaseStudyQuitQ.Data;
using CaseStudyQuitQ.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseStudyQuitQ.Repository {
    public class CartItemService : ICartItemService {
        private readonly QuitQEcomContext _context;
         
        public CartItemService(QuitQEcomContext context) {
            _context = context;
        }
        public async Task<int> AddNewCartItem(CartItem cartItem) {
            //if (cartItem == null) throw new ArgumentNullException(nameof(cartItem));

            var product = _context.Products.FirstOrDefault(x => x.Id == cartItem.ProductId);
            if (product == null || cartItem.Quantity > product.Stock) return -1;

            var existingCartItem=_context.CartItems.FirstOrDefault(x=>x.CartId==cartItem.CartId && x.ProductId == cartItem.ProductId);
            if (existingCartItem != null) {
                existingCartItem.Quantity += cartItem.Quantity;
            }
            else {
                _context.CartItems.Add(cartItem);
            }
            await _context.SaveChangesAsync();
            return existingCartItem?.Id??cartItem.Id;
        }

        public async Task<string> DeleteCartItem(int id) {
            //var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (id != null) {
                var CartItem = _context.CartItems.FirstOrDefault(x => x.Id == id);
                if (CartItem != null) {
                    //product.Stock += CartItem.Quantity;
                    _context.CartItems.Remove(CartItem);

                    _context.SaveChanges();
                    return "The given CartItem Id " + id + " is Removed";
                }
                else return null;
            }
            return null;
        }

        public async Task<List<CartItem>> GetAllCartItems() {
            var CartItem = _context.CartItems.ToList();
            if (CartItem.Count > 0) return CartItem;
            return null;
        }
            
        public async Task<CartItem> GetCartItemById(int id) {
            if (id != 0 || id != null) {
                var CartItem = _context.CartItems.FirstOrDefault(x => x.Id == id);
                if (CartItem != null) return CartItem;
                else return null;
            }
            return null;
        }

        public async Task<List<Object>> GetCartItemByCartId(int cartId) {
            if (cartId != 0 || cartId != null) {
                var cartItems = await _context.CartItems.Include(x=>x.Product).Where(x => x.CartId==cartId).Select(x=>new{
                    x.Id,
                    x.Quantity,
                Product = new
                {
                    x.Product.Id,
                    x.Product.Name,
                    x.Product.Description,
                    x.Product.Price,
                    x.Product.Stock,
                    x.Product.ImageUrl
                }
            }).ToListAsync();

                return cartItems.Count > 0 ? cartItems.Cast<object>().ToList() : null;

            }
            return null;
        }

        public async Task<CartItem> UpdateCartItem(CartItem cartItem) {

            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == cartItem.ProductId);
            var existingCartItem = await _context.CartItems.FirstOrDefaultAsync(x => x.Id == cartItem.Id);
            if (existingCartItem != null) {
                existingCartItem.CartId = cartItem.CartId;
                existingCartItem.ProductId = cartItem.ProductId;

                if (product.Stock >= cartItem.Quantity) {
                    existingCartItem.Quantity = cartItem.Quantity;
                    //product.Stock -= cartItem.Quantity;
                }
                else {
                    return null;
                }

                _context.Entry(existingCartItem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();
                return cartItem;

            }
            return null;
        }
    }
}
