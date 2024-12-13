using CaseStudyQuitQ.Data;
using CaseStudyQuitQ.Models;
using CaseStudyQuitQ.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuitQTest.ServiceTests {
    public class ProductServiceTest {
        private Mock<QuitQEcomContext> _mockContext;
        private ProductService _productService;
        private List<Product> _products;
        private List<ProductCategory> _productCategories;
        private List<SubCategory> _subCategories;

        [SetUp]
        public void Setup() {
            var options = new DbContextOptionsBuilder<QuitQEcomContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase").Options;

            _mockContext = new Mock<QuitQEcomContext>(options);
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Product1", Description = "Description1", Price = 100, Stock = 10, ImageUrl = "Url1", SellerId = 1, CategoryId = 1, SubCategoryId = 1 },
                new Product { Id = 2, Name = "Product2", Description = "Description2", Price = 200, Stock = 20, ImageUrl = "Url2", SellerId = 2, CategoryId = 1, SubCategoryId = 2 }
            };

            _productCategories = new List<ProductCategory>
            {
                new ProductCategory { Id = 1, Name = "Category1" },
                new ProductCategory { Id = 2, Name = "Category2" }
            };

            _subCategories = new List<SubCategory>
            {
                new SubCategory { Id = 1, Name = "SubCategory1" },
                new SubCategory { Id = 2, Name = "SubCategory2" }
            };

            var mockProductSet = CreateMockDbSet(_products);
            var mockCategorySet = CreateMockDbSet(_productCategories);
            var mockSubCategorySet = CreateMockDbSet(_subCategories);

            _mockContext.Setup(c => c.Products).Returns(mockProductSet.Object);
            _mockContext.Setup(c => c.ProductCategories).Returns(mockCategorySet.Object);
            _mockContext.Setup(c => c.SubCategories).Returns(mockSubCategorySet.Object);
            _mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            _productService = new ProductService(_mockContext.Object);
        }

        [Test]
        public async Task GetAllProducts_ShouldReturnAllProducts() {
            var result = await _productService.GetAllProducts();
            Assert.NotNull(result);
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task AddNewProduct_ShouldAddProductSuccessfully() {
            var newProduct = new Product
            {
                Name = "Product3",
                Description = "Description3",
                Price = 300,
                Stock = 30,
                ImageUrl = "Url3",
                SellerId = 3,
                CategoryId = 2,
                SubCategoryId = 2
            };

            var result = await _productService.AddNewProduct(newProduct);
            Assert.That(result, Is.EqualTo(newProduct.Id));
            _mockContext.Verify(m => m.Products.Add(It.IsAny<Product>()), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [Test]
        public async Task DeleteProduct_ShouldRemoveProduct() {
            var result = await _productService.DeleteProduct(1);
            Assert.That(result, Is.EqualTo("The given product Id 1 is Removed"));
            _mockContext.Verify(m => m.Products.Remove(It.IsAny<Product>()), Times.Once);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Test]
        public async Task DeleteProduct_ShouldReturnError_WhenProductNotFound() {
            var result = await _productService.DeleteProduct(3);
            Assert.IsNull(result);
            _mockContext.Verify(m => m.Products.Remove(It.IsAny<Product>()), Times.Never);
            _mockContext.Verify(m => m.SaveChanges(), Times.Never);
        }

        
        [Test]
        public async Task UpdateProduct_ShouldReturnNull_WhenProductNotFound() {
            var updateProduct = new Product
            {
                Id = 3,
                Name = "NonExistingProduct",
                Description = "NoDescription",
                Price = 400,
                Stock = 0,
                ImageUrl = "NoUrl",
                SellerId = 3,
                CategoryId = 2,
                SubCategoryId = 2
            };

            var result = await _productService.UpdateProduct(updateProduct);
            Assert.IsNull(result);
            _mockContext.Verify(m => m.SaveChanges(), Times.Never);
        }

        private Mock<DbSet<T>> CreateMockDbSet<T>(List<T> data) where T : class {
            var queryableData = data.AsQueryable();
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());
            return mockSet;
        }
    }
}
