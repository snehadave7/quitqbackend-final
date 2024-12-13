using CaseStudyQuitQ.Data;
using CaseStudyQuitQ.Models;

namespace CaseStudyQuitQ.Repository {
    public class ProductCategoryService : IProductCategoryService {
        private readonly QuitQEcomContext _context;

        public ProductCategoryService(QuitQEcomContext context) {
            _context = context;
        }
        public async Task<int> AddNewProductCategory(ProductCategory productCategory) {
            if (productCategory != null) {
                _context.ProductCategories.Add(productCategory);
                _context.SaveChanges();
                return productCategory.Id;
            }
            return 0;
        }

        public async Task<string> DeleteProductCategory(int id) {
            if (id != null) {
                var productCategory = _context.ProductCategories.FirstOrDefault(x => x.Id == id);
                if (productCategory != null) {
                    _context.ProductCategories.Remove(productCategory);
                    _context.SaveChanges();
                    return "The given productCategory Id " + id + " is Removed";
                }
                else return null;
            }
            return null;
        }

        public async Task<List<ProductCategory>> GetAllProductCategory() {
            var productCategory = _context.ProductCategories.ToList();
            if (productCategory.Count > 0) return productCategory;
            return null;
        }

        public async Task<ProductCategory> GetProductCategoryById(int id) {
            if (id != 0 || id != null) {
                var productCategory = _context.ProductCategories.FirstOrDefault(x => x.Id == id);
                if (productCategory != null) return productCategory;
                else return null;
            }
            return null;
        }

        public async Task<ProductCategory> UpdateProductCategory(ProductCategory productCategory) {
            var existingProductCategory = _context.ProductCategories.FirstOrDefault(x => x.Id == productCategory.Id);
            if (existingProductCategory != null) {
                
                _context.Entry(existingProductCategory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return productCategory;

            }
            return null;
        }
    }
}
