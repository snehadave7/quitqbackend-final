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
    internal class CartServiceTest {
        private Mock<QuitQEcomContext> _contextMock;
        private CartService _cartService;

        [SetUp]
        public void Setup() {
            _contextMock = new Mock<QuitQEcomContext>();
            _cartService = new CartService(_contextMock.Object);
        }

        [Test]
        public async Task AddNewCart_ValidCart_ReturnsCartId() {
            // Arrange
            var cart = new Cart { Id = 1, UserId = 1 };
            var cartSet = CreateMockDbSet(new List<Cart>().AsQueryable());

            _contextMock.Setup(c => c.Carts).Returns(cartSet.Object);

            // Act
            var result = await _cartService.AddNewCart(cart);

            // Assert
            Assert.AreEqual(1, result);
            cartSet.Verify(m => m.Add(It.Is<Cart>(c => c.Id == 1)), Times.Once);
            _contextMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Test]
        public async Task DeleteCart_ValidCartId_ReturnsSuccessMessage() {
            // Arrange
            var cart = new Cart { Id = 1, UserId = 1 };
            var cartSet = CreateMockDbSet(new List<Cart> { cart }.AsQueryable());

            _contextMock.Setup(c => c.Carts).Returns(cartSet.Object);

            // Act
            var result = await _cartService.DeleteCart(1);

            // Assert
            Assert.AreEqual("The given Cart Id 1 is Removed", result);
            cartSet.Verify(m => m.Remove(It.Is<Cart>(c => c.Id == 1)), Times.Once);
            _contextMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Test]
        public async Task GetAllCarts_ReturnsAllCarts() {
            // Arrange
            var carts = new List<Cart>
            {
                new Cart { Id = 1, UserId = 1 },
                new Cart { Id = 2, UserId = 2 }
            }.AsQueryable();

            var cartSet = CreateMockDbSet(carts);
            _contextMock.Setup(c => c.Carts).Returns(cartSet.Object);

            // Act
            var result = await _cartService.GetAllCarts();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public async Task GetCartById_ValidUserId_ReturnsCart() {
            // Arrange
            var cart = new Cart { Id = 1, UserId = 1 };
            var cartSet = CreateMockDbSet(new List<Cart> { cart }.AsQueryable());

            _contextMock.Setup(c => c.Carts).Returns(cartSet.Object);

            // Act
            var result = await _cartService.GetCartById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.UserId);
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




