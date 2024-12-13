using QuitQBackend.Models;

namespace QuitQBackend.Repositories {
    public interface IProductService {

        List<Product> GetAllProducts();
        Product GetProductById(int id);
        int AddNewProduct(Product product);
        Product UpdateProduct(Product product);
        string DeleteProduct(int id);
    }
}
