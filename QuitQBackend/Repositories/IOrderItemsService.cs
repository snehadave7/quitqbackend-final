using QuitQBackend.Models;

namespace QuitQBackend.Repositories {
    public interface IOrderItemsService {
        List<OrderItem> GetAllOrderItems();
        OrderItem GetOrderItemById(int id);
        int AddNewOrderItem(OrderItem orderItem);
        OrderItem UpdateOrderItem(OrderItem orderItem);
        string DeleteOrderItem(int id);
    }
}
