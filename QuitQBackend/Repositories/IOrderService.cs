using QuitQBackend.Models;

namespace QuitQBackend.Repositories {
    public interface IOrderService {
        List<Order> GetAllOrders();
        Order GetOrderById(int id);
        int AddNewOrder(Order Order);
        Order UpdateOrder(Order Order);
        string DeleteOrder(int id);
    }
}
