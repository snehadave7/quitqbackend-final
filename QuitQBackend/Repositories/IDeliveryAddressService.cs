using QuitQBackend.Models;

namespace QuitQBackend.Repositories {
    public interface IDeliveryAddressService {
        List<DeliveryAddress> GetAllDeliveryAddresss();
        DeliveryAddress GetDeliveryAddressById(int id);
        int AddNewDeliveryAddress(DeliveryAddress deliveryAddress);
        DeliveryAddress UpdateDeliveryAddress(DeliveryAddress deliveryAddress);
        string DeleteDeliveryAddress(int id);
    }
}
