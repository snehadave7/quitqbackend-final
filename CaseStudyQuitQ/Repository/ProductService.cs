using CaseStudyQuitQ.Data;
using CaseStudyQuitQ.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseStudyQuitQ.Repository {
    public class ProductService : IProductService {
        private readonly QuitQEcomContext _context;

        public ProductService(QuitQEcomContext context) {
            _context = context;
        }
        public async Task<int> AddNewProduct(Product product) {
            var productImageUrl = product.ImageUrl;

            if (product != null) {
                var newProduct = new Product
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock,
                    ImageUrl = productImageUrl,
                    SellerId = product.SellerId,
                    CategoryId = product.CategoryId,
                    SubCategoryId = product.SubCategoryId,
                };

                _context.Products.Add(newProduct);

                await _context.SaveChangesAsync();
                return product.Id;
            }
            else return 0;
        }

        public async Task<string> DeleteProduct(int id) {
            if (id != null) {
                var product = _context.Products.FirstOrDefault(x => x.Id == id);
                if (product != null) {
                    _context.Products.Remove(product);
                    _context.SaveChanges();
                    return "The given product Id " + id + " is Removed";
                }
                else return null;
            }
            return null;
        }

       
       

        public async Task<List<Product>> GetAllProducts() {
            var product = _context.Products.ToList();
            if (product.Count > 0) return product;
            return null;
        }

        public async Task<List<Product>> GetProductBySellerId(int sellerId) {
            if (sellerId != 0 || sellerId != null) {
                var product = _context.Products.Where(x => x.SellerId == sellerId).ToList();
                if (product != null) return product;
                else return null;
            }
            return null;
        }

        public async Task<List<Product>> SearchByProductCategory(string productCategory) {

            var product = _context.Products
                .Join(_context.ProductCategories,
                p => p.CategoryId,
                ps => ps.Id,
                (p, ps) => new { Product = p, ProductCategory = ps }
                )
                .Where(x => x.ProductCategory.Name.Contains(productCategory))
                .Select(x => x.Product)
                .Include(x=>x.Reviews)
                .ToList();

            if (product != null) return product;
            return null;
        }

        public async Task<List<Product>> SearchByProductName(string productName) {
            var product = _context.Products.Where(p => EF.Functions.Like(p.Name, $"%{productName}%")).ToList();
            if (product != null) return product;
            else return null;

        }

        public async Task<List<Product>> SearchBySubCategory(List<string> subCategories) {
            var product = await _context.Products
                .Join(
                _context.SubCategories,
                p => p.SubCategoryId,
                s => s.Id,
                (p, s) => new { Product = p, SubCategory = s }
                )
                //.Where(x => x.SubCategory.Name.Contains(SubCategory))
                .Where(x=>subCategories.Contains(x.SubCategory.Name))
                .Select(x => x.Product)
                .ToListAsync();

            if (product != null) return product;
            return null;

        }

      

        public async Task<Product> UpdateProduct(Product product) {
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
                return product;

            }
            return null;
        }
    }
}
