using QuitQBackend.Data;
using QuitQBackend.Models;

namespace QuitQBackend.Repositories {
    public class ProductService : IProductService {
        private readonly QuitQContext _context;

        public ProductService(QuitQContext context) {
            _context = context;
        }
        public int AddNewProduct(Product product) {
            if (product != null) {
                _context.Products.Add(product);
                _context.SaveChanges();
                return product.Id;
            }
            return 0;
        }

        public string DeleteProduct(int id) {
            if (id != null) {
                var product = _context.Products.FirstOrDefault(x => x.Id == id);
                if (product != null) {
                    _context.Products.Remove(product);
                    _context.SaveChanges();
                    return "The given product Id " + id + " is Removed";
                }
                else return "Something went wrong with deletion";
            }
            return null;
        }

        public List<Product> GetAllProducts() {
            var product = _context.Products.ToList();
            if (product.Count > 0) return product;
            return null;
        }

        public Product GetProductById(int id) {
            if (id != 0 || id != null) {
                var product = _context.Products.FirstOrDefault(x => x.Id == id);
                if (product != null) return product;
                else return null;
            }
            return null;
        }

        public Product UpdateProduct(Product product) {
            var existingproduct = _context.Products.FirstOrDefault(x => x.Id == product.Id);
            if (existingproduct != null) {
                existingproduct.Name = product.Name;
                existingproduct.Description = product.Description;
                existingproduct.Price = product.Price;
                existingproduct.Stock = product.Stock;
                existingproduct.ImageUrl = product.ImageUrl;
                existingproduct.SellerId = product.SellerId;
                existingproduct.CategoryId = product.CategoryId;
                existingproduct.SubCategoryId = product.SubCategoryId;
                _context.Entry(existingproduct).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();


            }
            return product;
        }
    }
}
