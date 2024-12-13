using CaseStudyQuitQ.Models;

namespace CaseStudyQuitQ.Repository {
    public interface IProductCategoryService {
        Task<List<ProductCategory>> GetAllProductCategory();
        Task<ProductCategory> GetProductCategoryById(int id);
        Task<int> AddNewProductCategory(ProductCategory productCategory);
        Task<ProductCategory> UpdateProductCategory(ProductCategory productCategory);
        Task<string> DeleteProductCategory(int id);
    }
}
