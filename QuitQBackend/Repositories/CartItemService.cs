using QuitQBackend.Data;
using QuitQBackend.Models;
using System.Diagnostics.Eventing.Reader;

namespace QuitQBackend.Repositories {
    public class CartItemService : ICartItemService {
        private readonly QuitQContext _context;

        public CartItemService(QuitQContext context) {
            _context = context;
        }
        public int AddNewCartItem(CartItem cartItem) {
            var product = _context.Products.FirstOrDefault(x => x.Id == cartItem.ProductId);
            if (cartItem != null) {
                if (product!=null && cartItem.Quantity <=product.Stock ){
                    _context.CartItems.Add(cartItem);
                    //product.Stock -= cartItem.Quantity;
                    _context.SaveChanges();
                    return cartItem.Id;
                }
                else {
                    return -1;
                }
            }
            return 0;
        }

        public string DeleteCartItem(int id) {
            //var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (id != null) {
                var CartItem = _context.CartItems.FirstOrDefault(x => x.Id == id);
                if (CartItem != null) {
                    //product.Stock += CartItem.Quantity;
                    _context.CartItems.Remove(CartItem);

                    _context.SaveChanges();
                    return "The given CartItem Id " + id + " is Removed";
                }
                else return "Something went wrong with deletion";
            }
            return null;
        }

        public List<CartItem> GetAllCartItems() {
            var CartItem = _context.CartItems.ToList();
            if (CartItem.Count > 0) return CartItem;
            return null;
        }
            
        public CartItem GetCartItemById(int id) {
            if (id != 0 || id != null) {
                var CartItem = _context.CartItems.FirstOrDefault(x => x.Id == id);
                if (CartItem != null) return CartItem;
                else return null;
            }
            return null;
        }

        public CartItem UpdateCartItem(CartItem cartItem) {

            var product = _context.Products.FirstOrDefault(x => x.Id == cartItem.ProductId);
            var existingCartItem = _context.CartItems.FirstOrDefault(x => x.Id == cartItem.Id);
            if (existingCartItem != null) {
                existingCartItem.CartId = cartItem.Id;
                existingCartItem.ProductId = cartItem.ProductId;
                if (product.Stock >= cartItem.Quantity) {
                    existingCartItem.Quantity = cartItem.Quantity;
                    //product.Stock -= cartItem.Quantity;
                }
                else {
                    return null;
                }

                _context.Entry(existingCartItem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();


            }
            return cartItem;
        }
    }
}
