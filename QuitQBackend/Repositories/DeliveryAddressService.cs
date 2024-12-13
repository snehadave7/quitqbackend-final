using Microsoft.EntityFrameworkCore;
using QuitQBackend.Data;
using QuitQBackend.Models;

namespace QuitQBackend.Repositories {
    public class DeliveryAddressService : IDeliveryAddressService {
        private readonly QuitQContext _context;

        public DeliveryAddressService(QuitQContext context) {
            _context = context;
        }
        public int AddNewDeliveryAddress(DeliveryAddress deliveryAddress) {
            if (deliveryAddress != null) {
                _context.DeliveryAddresses.Add(deliveryAddress);
                _context.SaveChanges();
                return deliveryAddress.Id;
            }
            return 0;
        }

        public string DeleteDeliveryAddress(int id) {
            if (id != null) {
                var DeliveryAddress = _context.DeliveryAddresses.FirstOrDefault(x => x.Id == id);
                if (DeliveryAddress != null) {
                    _context.DeliveryAddresses.Remove(DeliveryAddress);
                    _context.SaveChanges();
                    return "The given DeliveryAddress Id " + id + " is Removed";
                }
                else return "Something went wrong with deletion";
            }
            return null;
        }

        public List<DeliveryAddress> GetAllDeliveryAddresss() {
            var deliveryAddress = _context.DeliveryAddresses.Include(U=>U.User).ToList();
            if (deliveryAddress.Count > 0) return deliveryAddress;
            return null;
        }

        public DeliveryAddress GetDeliveryAddressById(int id) {
            if (id != 0 || id != null) {
                var DeliveryAddress = _context.DeliveryAddresses.Include(U => U.User).FirstOrDefault(x => x.Id == id);
                if (DeliveryAddress != null) return DeliveryAddress;
                else return null;
            }
            return null;
        }

        public DeliveryAddress UpdateDeliveryAddress(DeliveryAddress deliveryAddress) {
            var existingDeliveryAddress = _context.DeliveryAddresses.FirstOrDefault(x => x.Id == deliveryAddress.Id);
            if (existingDeliveryAddress != null) {
                existingDeliveryAddress.UserId = deliveryAddress.UserId;
                existingDeliveryAddress.Address = deliveryAddress.Address;
                existingDeliveryAddress.City = deliveryAddress.City;
                existingDeliveryAddress.Pincode = deliveryAddress.Pincode;
                existingDeliveryAddress.Phone = deliveryAddress.Phone;
                existingDeliveryAddress.Notes = deliveryAddress.Notes;
                _context.Entry(existingDeliveryAddress).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();


            }
            return deliveryAddress;
        }
    }
}