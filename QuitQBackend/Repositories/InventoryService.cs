using QuitQBackend.Data;
using QuitQBackend.Models;

namespace QuitQBackend.Repositories {
    public class InventoryService:IInventoryService {
        private readonly QuitQContext _context;

        public InventoryService(QuitQContext context) {
            _context = context;
        }
        public int AddNewInventory(Inventory inventory) {
            if (inventory != null) {
                _context.Inventories.Add(inventory);
                _context.SaveChanges();
                return inventory.Id;
            }
            return 0;
        }

        public string DeleteInventory(int id) {
            if (id != null) {
                var Inventory = _context.Inventories.FirstOrDefault(x => x.Id == id);
                if (Inventory != null) {
                    _context.Inventories.Remove(Inventory);
                    _context.SaveChanges();
                    return "The given Inventory Id " + id + " is Removed";
                }
                else return "Something went wrong with deletion";
            }
            return null;
        }

        public List<Inventory> GetAllInventorys() {
            var Inventory = _context.Inventories.ToList();
            if (Inventory.Count > 0) return Inventory;
            return null;
        }

        public Inventory GetInventoryById(int id) {
            if (id != 0 || id != null) {
                var Inventory = _context.Inventories.FirstOrDefault(x => x.Id == id);
                if (Inventory != null) return Inventory;
                else return null;
            }
            return null;
        }

        public Inventory UpdateInventory(Inventory inventory) {
            var existingInventory = _context.Inventories.FirstOrDefault(x => x.Id == inventory.Id);
            if (existingInventory != null) {
                existingInventory.ProductId = inventory.ProductId;
                existingInventory.StockQuantity = inventory.StockQuantity;
                _context.Entry(existingInventory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();


            }
            return inventory;
        }
    }
}
