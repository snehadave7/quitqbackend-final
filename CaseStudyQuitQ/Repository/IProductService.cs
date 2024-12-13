using CaseStudyQuitQ.Models;

namespace CaseStudyQuitQ.Repository {
    public interface IProductService {

        Task<List<Product>> GetAllProducts();
        Task<List<Product>> GetProductBySellerId(int sellerid);
        Task<int> AddNewProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<string> DeleteProduct(int id);

        Task<List<Product>> SearchByProductName(string productName);
        Task<List<Product>> SearchByProductCategory(string productCategory);
        Task<List<Product>> SearchBySubCategory(List<string> subCategories);

        


    }
}
