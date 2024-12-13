using QuitQBackend.Models;

namespace QuitQBackend.Repositories {
    public interface IProductCategoryService {
        List<ProductCategory> GetAllProductCategory();
        ProductCategory GetProductCategoryById(int id);
        int AddNewProductCategory(ProductCategory productCategory);
        ProductCategory UpdateProductCategory(ProductCategory productCategory);
        string DeleteProductCategory(int id);
    }
}
