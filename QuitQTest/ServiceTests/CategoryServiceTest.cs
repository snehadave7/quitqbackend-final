using CaseStudyQuitQ.Data;
using CaseStudyQuitQ.Models;
using CaseStudyQuitQ.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuitQTest.ServiceTests {
    internal class CategoryServiceTest {

        private Mock<QuitQEcomContext> _contextMock;
        private ProductCategoryService _productCategoryService;

        [SetUp]
        public void Setup() {
            _contextMock = new Mock<QuitQEcomContext>();
            _productCategoryService = new ProductCategoryService(_contextMock.Object);
        }

        [Test]
        public async Task AddNewProductCategory_ValidProductCategory_ReturnsId() {
            // Arrange
            var productCategory = new ProductCategory { Id = 1, Name = "Electronics" };
            var productCategorySet = CreateMockDbSet(new List<ProductCategory>().AsQueryable());

            _contextMock.Setup(c => c.ProductCategories).Returns(productCategorySet.Object);

            // Act
            var result = await _productCategoryService.AddNewProductCategory(productCategory);

            // Assert
            Assert.AreEqual(1, result);
            productCategorySet.Verify(m => m.Add(It.Is<ProductCategory>(pc => pc.Id == 1)), Times.Once);
            _contextMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Test]
        public async Task DeleteProductCategory_ValidId_ReturnsSuccessMessage() {
            // Arrange
            var productCategory = new ProductCategory { Id = 1, Name = "Electronics" };
            var productCategorySet = CreateMockDbSet(new List<ProductCategory> { productCategory }.AsQueryable());

            _contextMock.Setup(c => c.ProductCategories).Returns(productCategorySet.Object);

            // Act
            var result = await _productCategoryService.DeleteProductCategory(1);

            // Assert
            Assert.AreEqual("The given productCategory Id 1 is Removed", result);
            productCategorySet.Verify(m => m.Remove(It.Is<ProductCategory>(pc => pc.Id == 1)), Times.Once);
            _contextMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Test]
        public async Task GetAllProductCategory_ReturnsAllCategories() {
            // Arrange
            var productCategories = new List<ProductCategory>
            {
                new ProductCategory { Id = 1, Name = "Electronics" },
                new ProductCategory { Id = 2, Name = "Books" }
            }.AsQueryable();

            var productCategorySet = CreateMockDbSet(productCategories);
            _contextMock.Setup(c => c.ProductCategories).Returns(productCategorySet.Object);

            // Act
            var result = await _productCategoryService.GetAllProductCategory();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public async Task GetProductCategoryById_ValidId_ReturnsProductCategory() {
            // Arrange
            var productCategory = new ProductCategory { Id = 1, Name = "Electronics" };
            var productCategorySet = CreateMockDbSet(new List<ProductCategory> { productCategory }.AsQueryable());

            _contextMock.Setup(c => c.ProductCategories).Returns(productCategorySet.Object);

            // Act
            var result = await _productCategoryService.GetProductCategoryById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Electronics", result.Name);
        }

        

        private static Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : class {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet;
        }
    }
}



