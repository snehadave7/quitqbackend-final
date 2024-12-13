using QuitQBackend.Models;

namespace QuitQBackend.Repositories {
    public interface IInventoryService {
        List<Inventory> GetAllInventorys();
        Inventory GetInventoryById(int id);
        int AddNewInventory(Inventory inventory);
        Inventory UpdateInventory(Inventory inventory);
        string DeleteInventory(int id);
    }
}
