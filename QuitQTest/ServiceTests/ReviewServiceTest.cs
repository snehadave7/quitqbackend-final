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
    internal class ReviewServiceTest {
        private Mock<QuitQEcomContext> _contextMock;
        private ReviewService _reviewService;

        [SetUp]
        public void Setup() {
            _contextMock = new Mock<QuitQEcomContext>();
            _reviewService = new ReviewService(_contextMock.Object);
        }

        [Test]
        public async Task AddNewReview_ValidReview_ReturnsReviewId() {
            // Arrange
            var review = new Review { Id = 1, Rating = 5, Comment = "Excellent product!" };
            var mockSet = CreateMockDbSet(new List<Review>().AsQueryable());

            _contextMock.Setup(c => c.Reviews).Returns(mockSet.Object);

            // Act
            var result = await _reviewService.AddNewReview(review);

            // Assert
            Assert.AreEqual(1, result);
            mockSet.Verify(m => m.Add(It.Is<Review>(r => r.Id == 1)), Times.Once);
            _contextMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Test]
        public async Task DeleteReview_ReviewExists_ReturnsSuccessMessage() {
            // Arrange
            var review = new Review { Id = 1, Rating = 5 };
            var reviews = new List<Review> { review }.AsQueryable();
            var mockSet = CreateMockDbSet(reviews);

            _contextMock.Setup(c => c.Reviews).Returns(mockSet.Object);

            // Act
            var result = await _reviewService.DeleteReview(1);

            // Assert
            Assert.AreEqual("The given Review Id 1 is Removed", result);
            mockSet.Verify(m => m.Remove(It.Is<Review>(r => r.Id == 1)), Times.Once);
            _contextMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Test]
        public async Task GetAllReviews_SellerIdExists_ReturnsReviews() {
            // Arrange
            var sellerId = 1;
            var products = new List<Product>
            {
                new Product { Id = 1, SellerId = sellerId },
                new Product { Id = 2, SellerId = sellerId }
            }.AsQueryable();
            var reviews = new List<Review>
            {
                new Review { Id = 1, ProductId = 1, Rating = 4 },
                new Review { Id = 2, ProductId = 2, Rating = 5 }
            }.AsQueryable();

            var productMockSet = CreateMockDbSet(products);
            var reviewMockSet = CreateMockDbSet(reviews);

            _contextMock.Setup(c => c.Products).Returns(productMockSet.Object);
            _contextMock.Setup(c => c.Reviews).Returns(reviewMockSet.Object);

            // Act
            var result = await _reviewService.GetAllReviews(sellerId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public async Task GetReviewByProductId_ProductIdExists_ReturnsReviews() {
            // Arrange
            var productId = 1;
            var reviews = new List<Review>
            {
                new Review { Id = 1, ProductId = productId, Rating = 5 },
                new Review { Id = 2, ProductId = productId, Rating = 4 }
            }.AsQueryable();
            var mockSet = CreateMockDbSet(reviews);

            _contextMock.Setup(c => c.Reviews).Returns(mockSet.Object);

            // Act
            var result = await _reviewService.GetReviewByProductId(productId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
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


