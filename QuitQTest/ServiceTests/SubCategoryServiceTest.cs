using CaseStudyQuitQ.Data;
using CaseStudyQuitQ.Models;
using CaseStudyQuitQ.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace QuitQTest.ServiceTests {
    internal class SubCategoryServiceTest {

        private Mock<QuitQEcomContext> _mockContext;
        private SubCategoryService _service;
        private List<SubCategory> _subCategories;

        [SetUp]
        public void Setup() {
            var options = new DbContextOptionsBuilder<QuitQEcomContext>().
                UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            _mockContext = new Mock<QuitQEcomContext>(options);
            _subCategories = new List<SubCategory>
        {
            new SubCategory { Id = 1, Name = "HR", CategoryId = 1 },
            new SubCategory { Id = 2, Name = "Finance", CategoryId = 2 }
        };

            var mockSubCategorySet = new Mock<DbSet<SubCategory>>();
            var queryableSubCategory = _subCategories.AsQueryable();

            mockSubCategorySet.As<IQueryable<SubCategory>>().Setup(m => m.Provider).Returns(queryableSubCategory.Provider);
            mockSubCategorySet.As<IQueryable<SubCategory>>().Setup(m => m.Expression).Returns(queryableSubCategory.Expression);
            mockSubCategorySet.As<IQueryable<SubCategory>>().Setup(m => m.ElementType).Returns(queryableSubCategory.ElementType);
            mockSubCategorySet.As<IQueryable<SubCategory>>().Setup(m => m.GetEnumerator()). Returns(queryableSubCategory.GetEnumerator());



            _mockContext.Setup(c => c.SubCategories).Returns(mockSubCategorySet.Object);
            _service = new SubCategoryService(_mockContext.Object);

        }

        [Test]
        public async Task GetAllSubCategory_Should_AllSubCategory() {
            var result = await _service.GetAllSubCategorys();
            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(2));
            
        }
        [Test]
        public async Task AddNewSubCategory_shouldReturnAddSubCategoryId() {
            var newSubCategory = new SubCategory { Id = 3, Name = "Sales", CategoryId = 1 };
            var result = await _service.AddNewSubCategory(newSubCategory);
            Assert.That(result, Is.EqualTo(3));
            _mockContext.Verify(m => m.SubCategories.Add(It.IsAny<SubCategory>()), Times.Once);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }
        [Test]
        public async Task DeleteSubCategory_shouldRemoveSubCategory() {

            var result = await _service.DeleteSubCategory(1);
            Assert.That(result, Is.EqualTo("The given SubCategory Id 1 is Removed"));
            _mockContext.Verify(m => m.SubCategories.Remove(It.IsAny<SubCategory>()), Times.Once);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }
        [Test] 
        public async Task DeleteSubCategory_ShouldReturnError_WhenSubCategoryIdNotFound() {

            var result = await _service.DeleteSubCategory(3);
            Assert.IsNull(result);

            _mockContext.Verify(m => m.SubCategories.Remove(It.IsAny<SubCategory>()), Times.Never);
            _mockContext.Verify(m => m.SaveChanges(), Times.Never);
        }
        [Test]
        public async Task UpdateSubCategory_ShouldReturnError_WhenSubCategoryDoesNotExists() {
            var updateSubCategory = new SubCategory { Id = 3, Name = "No SubCategory", CategoryId=3 };
            var result =await _service.UpdateSubCategory(updateSubCategory);
            Assert.IsNull(result);
            _mockContext.Verify(m => m.SaveChanges(), Times.Never); 

        }
        [Test]
        public async Task UpdateSubCategory_ShouldUpdateSubCategory_WhenSubCategoryExists() {
            var updateSubCategory = new SubCategory { Id = 1, Name = "Updated SubCategory", CategoryId = 1 };
            _mockContext.Setup(m => m.SubCategories.FindAsync(It.IsAny<int>())).ReturnsAsync(new SubCategory { Id=1, Name="Original SubCategory",CategoryId=1});
            var result = await _service.UpdateSubCategory(updateSubCategory);
            Assert.IsNotNull(result,"Result is null");
            Assert.That(result,Is.EqualTo(updateSubCategory));
            
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);

        }

    }
}

