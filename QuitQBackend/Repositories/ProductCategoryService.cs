using QuitQBackend.Data;
using QuitQBackend.Models;

namespace QuitQBackend.Repositories {
    public class ProductCategoryService : IProductCategoryService {
        private readonly QuitQContext _context;

        public ProductCategoryService(QuitQContext context) {
            _context = context;
        }
        public int AddNewProductCategory(ProductCategory productCategory) {
            if (productCategory != null) {
                _context.ProductCategories.Add(productCategory);
                _context.SaveChanges();
                return productCategory.Id;
            }
            return 0;
        }

        public string DeleteProductCategory(int id) {
            if (id != null) {
                var productCategory = _context.ProductCategories.FirstOrDefault(x => x.Id == id);
                if (productCategory != null) {
                    _context.ProductCategories.Remove(productCategory);
                    _context.SaveChanges();
                    return "The given productCategory Id " + id + " is Removed";
                }
                else return "Something went wrong with deletion";
            }
            return null;
        }

        public List<ProductCategory> GetAllProductCategory() {
            var productCategory = _context.ProductCategories.ToList();
            if (productCategory.Count > 0) return productCategory;
            return null;
        }

        public ProductCategory GetProductCategoryById(int id) {
            if (id != 0 || id != null) {
                var productCategory = _context.ProductCategories.FirstOrDefault(x => x.Id == id);
                if (productCategory != null) return productCategory;
                else return null;
            }
            return null;
        }

        public ProductCategory UpdateProductCategory(ProductCategory productCategory) {
            var existingProductCategory = _context.ProductCategories.FirstOrDefault(x => x.Id == productCategory.Id);
            if (existingProductCategory != null) {
                
                _context.Entry(existingProductCategory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();


            }
            return productCategory;
        }
    }
}
