using CaseStudyQuitQ.Models;

namespace CaseStudyQuitQ.Repository {
    public interface IOrderService {
        Task<List<Order>> GetAllOrders();
        Task<Order> GetOrderById(int id);
        Task<List<Object>> GetOrderByUserId(int id);
        Task<List<Object>> GetOrderBySellerId(int id);
        Task<List<Object>> GetSalesReportForSellerAsync(int sellerId);
        Task<int> AddNewOrder(Order Order);
        Task<Order> UpdateOrder(Order Order);
        Task<string> DeleteOrder(int id);
        
    }
}
