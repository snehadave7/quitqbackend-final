using QuitQBackend.Data;
using QuitQBackend.Models;

namespace QuitQBackend.Repositories {
    public class OrderService : IOrderService {
        private readonly QuitQContext _context;

        public OrderService(QuitQContext context) {
            _context = context;
        }
        public int AddNewOrder(Order order) {
            
            if (order != null) {
                _context.Orders.Add(order);
                _context.SaveChanges();
                return order.Id;
            }
            return 0;
        }

        public string DeleteOrder(int id) {
            if (id != null) {
                var Order = _context.Orders.FirstOrDefault(x => x.Id == id);
                if (Order != null) {
                    _context.Orders.Remove(Order);
                    _context.SaveChanges();
                    return "The given Order Id " + id + " is Removed";
                }
                else return "Something went wrong with deletion";
            }
            return null;
        }

        public List<Order> GetAllOrders() {
            var Order = _context.Orders.ToList();
            if (Order.Count > 0) return Order;
            return null;
        }

        public Order GetOrderById(int id) {
            if (id != 0 || id != null) {
                var Order = _context.Orders.FirstOrDefault(x => x.Id == id);
                if (Order != null) return Order;
                else return null;
            }
            return null;
        }

        public Order UpdateOrder(Order Order) {
            var existingOrder = _context.Orders.FirstOrDefault(x => x.Id == Order.Id);
            if (existingOrder != null) {
                existingOrder.UserId=Order.UserId;
                existingOrder.Quantity=Order.Quantity;
                existingOrder.OrderDate=Order.OrderDate;
                _context.Entry(existingOrder).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();


            }
            return Order;
        }
    }
}
