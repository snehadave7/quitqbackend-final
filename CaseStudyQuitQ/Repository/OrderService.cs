using CaseStudyQuitQ.Data;
using CaseStudyQuitQ.Models;
using Microsoft.EntityFrameworkCore;





namespace CaseStudyQuitQ.Repository {
    public class OrderService : IOrderService {
        private readonly QuitQEcomContext _context;

        public OrderService(QuitQEcomContext context) {
            _context = context;
        }
        public async Task<int> AddNewOrder(Order order) {
            var product = _context.Products.FirstOrDefault(x => x.Id == order.ProductId);
            if (order != null) {
                if (product != null && order.Quantity <= product.Stock) {
                    _context.Orders.Add(order);
                    product.Stock -= order.Quantity;
                }
                else return -1;

                //var carItemToDelete = (from cart in _context.Carts
                //                       join cartItem in _context.CartItems on cart.Id equals cartItem.CartId
                //                       where cart.UserId == order.UserId
                //                       select cartItem).FirstOrDefault();
                var cartItemToDelete = _context.CartItems
                                    .FirstOrDefault(ci =>
                                    ci.ProductId == order.ProductId &&
                                    ci.Cart.UserId == order.UserId);

                if (cartItemToDelete != null) _context.CartItems.Remove(cartItemToDelete);

                _context.SaveChanges();
                return order.Id;
            }
            return 0;
        }

        public async Task<string> DeleteOrder(int id) {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (id != null) {
                var Order = _context.Orders.FirstOrDefault(x => x.Id == id);
                if (Order != null) {
                    product.Stock += Order.Quantity;
                    _context.Orders.Remove(Order);
                    _context.SaveChanges();
                    return "The given Order Id " + id + " is Removed";
                }
                else return null;
            }
            return null;
        }

        public async Task<List<Order>> GetAllOrders() {
            var Order = _context.Orders.Include(x=>x.Product).ToList();
            if (Order.Count > 0) return Order;
            return null;
        }

        public async Task<Order> GetOrderById(int id) {
            if (id != 0 || id != null) {
                var Order = _context.Orders.FirstOrDefault(x => x.Id == id);
                if (Order != null) return Order;
                else return null;
            }
            return null;
        }

        public async Task<List<Object>> GetOrderBySellerId(int id) {
            if (id != 0 || id != null) {
                var Order = await _context.Payments.Include(x=>x.Order).Include(x => x.Order.Product).Include(x => x.Order.Address).Include(x => x.Order.User).Where(x=>x.Order.Product.SellerId==id).Select(x => new
             

                {
                    x.Status,
                    x.Method,

                    Order = new
                    {
                        x.Order.Id,
                        x.Order.OrderStatus,
                        x.Order.OrderDate,
                        x.Order.Quantity,
                        Product = new

                        {
                            x.Order.Product.Id,
                            x.Order.Product.Name,
                            x.Order.Product.Price,
                            x.Order.Product.ImageUrl
                        },
                        Address = new
                        {
                            x.Order.Address.Id,
                            x.Order.Address.Address,
                            x.Order.Address.City,
                            x.Order.Address.Pincode,
                            x.Order.Address.Phone,
                            x.Order.Address.Notes
                        },
                        User = new
                        {
                            x.Order.User.Id,
                            x.Order.User.FirstName,
                            x.Order.User.LastName
                        },
                    }
                    

                }).ToListAsync();

                return Order.Count > 0 ? Order.Cast<object>().ToList() : null;
            }
            return null;
        }





        public async Task<List<Object>> GetOrderByUserId(int id) {
            if (id != 0 || id != null) {
                var Order = await _context.Orders.Include(x => x.Product).Include(x => x.Address).Include(x => x.User).Where(x => x.UserId == id).Select(x => new
                {
                    x.Id,
                    x.OrderStatus,
                    x.OrderDate,
                    x.Quantity,
                    Product = new
                    {
                        x.Product.Name,
                        x.Product.Price,
                        x.Product.ImageUrl
                    },
                    Address = new
                    {
                        x.Address.Address,
                        x.Address.City,
                        x.Address.Pincode,
                        x.Address.Phone,
                        x.Address.Notes
                    },
                    User = new
                    {
                        x.User.FirstName,
                        x.User.LastName
                    }

                }).ToListAsync();

                return Order.Count > 0 ? Order.Cast<object>().ToList() : null;
            }
            return null;
        }


        public async Task<Order> UpdateOrder(Order Order) {
            var product = _context.Products.FirstOrDefault(x => x.Id == Order.ProductId);
            var existingOrder = _context.Orders.FirstOrDefault(x => x.Id == Order.Id);
            if (existingOrder != null) {
                existingOrder.UserId=Order.UserId;
                existingOrder.ProductId=Order.ProductId;
                if (product.Stock >= Order.Quantity) {
                    existingOrder.Quantity = Order.Quantity;
                    product.Stock -= existingOrder.Quantity;
                }
                else {
                    return null;
                }
                existingOrder.OrderDate=Order.OrderDate;
                existingOrder.OrderStatus = Order.OrderStatus;
                _context.Entry(existingOrder).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return Order;

            }
            return null;
        }

        
        
        public async Task<List<Object>> GetSalesReportForSellerAsync(int sellerId) {
            var salesReport = await (from order in _context.Orders
                                     join product in _context.Products
                                     on order.ProductId equals product.Id
                                     where product.SellerId == sellerId // Filter by SellerId
                                     group new { order, product } by new
                                     {
                                         product.Id,
                                         product.Name,
                                         product.Price
                                     } into g
                                     orderby g.Sum(x => x.order.Quantity * x.product.Price) descending
                                     select new 
                                     {
                                         ProductId = g.Key.Id,
                                         ProductName = g.Key.Name,
                                         ProductPrice = g.Key.Price,
                                         TotalQuantitySold = g.Sum(x => x.order.Quantity),
                                         TotalSalesRevenue = g.Sum(x => x.order.Quantity * x.product.Price),
                                         TotalOrders = g.Count()
                                     }).ToListAsync();

            return salesReport.Cast<Object>().ToList();
        }

    }
}
