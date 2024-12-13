using CaseStudyQuitQ.Data;
using CaseStudyQuitQ.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseStudyQuitQ.Repository {
    public class DeliveryAddressService : IDeliveryAddressService {
        private readonly QuitQEcomContext _context;

        public DeliveryAddressService(QuitQEcomContext context) {
            _context = context;
        }
        public async Task<int> AddNewDeliveryAddress(DeliveryAddress deliveryAddress) {
            if (deliveryAddress != null) {
                _context.DeliveryAddresses.Add(deliveryAddress);
                _context.SaveChanges();
                return deliveryAddress.Id;
            }
            return 0;
        }

        public async Task<string> DeleteDeliveryAddress(int id) {
            if (id != null) {
                var DeliveryAddress = _context.DeliveryAddresses.FirstOrDefault(x => x.Id == id);
                if (DeliveryAddress != null) {
                    _context.DeliveryAddresses.Remove(DeliveryAddress);
                    _context.SaveChanges();
                    return "The given DeliveryAddress Id " + id + " is Removed";
                }
                else return null;
            }
            return null;
        }

        public async Task<List<DeliveryAddress>> GetAllDeliveryAddresss(int userId) {
            var deliveryAddress = _context.DeliveryAddresses.Where(x=>x.UserId==userId).ToList();
            if (deliveryAddress.Count > 0) return deliveryAddress;
            return null;
        }

        public async Task<DeliveryAddress> GetDeliveryAddressById(int id) {
            if (id != 0 || id != null) {
                var DeliveryAddress = _context.DeliveryAddresses.FirstOrDefault(x => x.Id == id);
                if (DeliveryAddress != null) return DeliveryAddress;
                else return null;
            }
            return null;
        }

        public async Task<DeliveryAddress> UpdateDeliveryAddress(DeliveryAddress deliveryAddress) {
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
                return deliveryAddress;

            }
            return null;
        }
    }
}