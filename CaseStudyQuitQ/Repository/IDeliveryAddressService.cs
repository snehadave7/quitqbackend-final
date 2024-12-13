using CaseStudyQuitQ.Models;

namespace CaseStudyQuitQ.Repository {
    public interface IDeliveryAddressService {
        Task<List<DeliveryAddress>> GetAllDeliveryAddresss(int userId);
        Task<DeliveryAddress> GetDeliveryAddressById(int id);
        Task<int> AddNewDeliveryAddress(DeliveryAddress deliveryAddress);
        Task<DeliveryAddress> UpdateDeliveryAddress(DeliveryAddress deliveryAddress);
        Task<string> DeleteDeliveryAddress(int id);
    }
}
