using Microsoft.EntityFrameworkCore;
using QuitQBackend.Data;
using QuitQBackend.Models;

namespace QuitQBackend.Repositories {
    public class OrderItemsService : IOrderItemsService {
        private readonly QuitQContext _context;

        public OrderItemsService(QuitQContext context) {
            _context = context;
        }
        public int AddNewOrderItem(OrderItem orderItem) {
            var orders=_context.Orders.FirstOrDefault(x=>x.Id == orderItem.OrderId);
            var product = _context.Products.FirstOrDefault(x => x.Id == orderItem.ProductId);

            if (orderItem != null) {
                if (product!=null && orderItem.Quantity <= product.Stock) {
                    _context.OrderItems.Add(orderItem);
                    product.Stock -= orderItem.Quantity;
                    //orders.Quantity += 1;
                    _context.SaveChanges();
                    return orderItem.Id;
                }
                else {
                    return -1;
                }
            }
            return 0;
        }

        public string DeleteOrderItem(int id) {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            var orders = _context.Orders.FirstOrDefault(x => x.Id == id);

            if (id != null) {
                var OrderItem = _context.OrderItems.FirstOrDefault(x => x.Id == id);
                if (OrderItem != null && product!=null && orders!=null) {
                    product.Stock += OrderItem.Quantity;
                    //orders.Quantity -= 1;
                    _context.OrderItems.Remove(OrderItem);
                    _context.SaveChanges();
                    return "The given OrderItem Id " + id + " is Removed";
                }
                else return "Something went wrong with deletion";
            }
            return null;
        }

        public List<OrderItem> GetAllOrderItems() {
            var OrderItem = _context.OrderItems.Include(O=>O.Order).Include(P=>P.Product).ToList();
            if (OrderItem.Count > 0) return OrderItem;
            return null;
        }

        public OrderItem GetOrderItemById(int id) {
            if (id != 0 || id != null) {
                var OrderItem = _context.OrderItems.Include(O => O.Order).Include(P => P.Product).FirstOrDefault(x => x.Id == id);
                if (OrderItem != null) return OrderItem;
                else return null;
            }
            return null;
        }

        public OrderItem UpdateOrderItem(OrderItem orderItem) {
            var product = _context.Products.FirstOrDefault(x => x.Id == orderItem.ProductId);

            var existingOrderItem = _context.OrderItems.FirstOrDefault(x => x.Id == orderItem.Id);
            if (existingOrderItem != null) {
                existingOrderItem.OrderId = orderItem.OrderId;
                existingOrderItem.ProductId = orderItem.ProductId;
                if (product.Stock >= orderItem.Quantity) {
                    existingOrderItem.Quantity = orderItem.Quantity;
                }
                else return null;

                existingOrderItem.OrderStatus = orderItem.OrderStatus;
                _context.Entry(existingOrderItem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();


            }
            return orderItem;
        }
    }
}
