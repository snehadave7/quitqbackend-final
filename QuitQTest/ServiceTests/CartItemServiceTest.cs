using CaseStudyQuitQ.Data;
using CaseStudyQuitQ.Models;
using CaseStudyQuitQ.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuitQTest.ServiceTests {
    public class CartItemServiceTest {
        private Mock<QuitQEcomContext> _contextMock;
        private CartItemService _cartItemService;

        [SetUp]
        public void Setup() {
            _contextMock = new Mock<QuitQEcomContext>();
            _cartItemService = new CartItemService(_contextMock.Object);
        }

        [Test]
        public async Task AddNewCartItem_ValidCartItem_ReturnsCartItemId() {
            // Arrange
            var product = new Product { Id = 1, Stock = 10 };
            var cartItem = new CartItem { Id = 1, CartId = 1, ProductId = 1, Quantity = 5 };

            var productSet = CreateMockDbSet(new List<Product> { product }.AsQueryable());
            var cartItemSet = CreateMockDbSet(new List<CartItem>().AsQueryable());

            _contextMock.Setup(c => c.Products).Returns(productSet.Object);
            _contextMock.Setup(c => c.CartItems).Returns(cartItemSet.Object);

            // Act
            var result = await _cartItemService.AddNewCartItem(cartItem);

            // Assert
            Assert.AreEqual(1, result);
            cartItemSet.Verify(m => m.Add(It.Is<CartItem>(ci => ci.Id == 1)), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }

        [Test]
        public async Task DeleteCartItem_ValidId_ReturnsSuccessMessage() {
            // Arrange
            var cartItem = new CartItem { Id = 1, CartId = 1, ProductId = 1, Quantity = 5 };
            var cartItemSet = CreateMockDbSet(new List<CartItem> { cartItem }.AsQueryable());

            _contextMock.Setup(c => c.CartItems).Returns(cartItemSet.Object);

            // Act
            var result = await _cartItemService.DeleteCartItem(1);

            // Assert
            Assert.AreEqual("The given CartItem Id 1 is Removed", result);
            cartItemSet.Verify(m => m.Remove(It.Is<CartItem>(ci => ci.Id == 1)), Times.Once);
            _contextMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Test]
        public async Task GetAllCartItems_ReturnsAllCartItems() {
            // Arrange
            var cartItems = new List<CartItem>
            {
                new CartItem { Id = 1, CartId = 1, ProductId = 1, Quantity = 2 },
                new CartItem { Id = 2, CartId = 1, ProductId = 2, Quantity = 3 }
            }.AsQueryable();

            var cartItemSet = CreateMockDbSet(cartItems);
            _contextMock.Setup(c => c.CartItems).Returns(cartItemSet.Object);

            // Act
            var result = await _cartItemService.GetAllCartItems();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public async Task GetCartItemById_ValidId_ReturnsCartItem() {
            // Arrange
            var cartItem = new CartItem { Id = 1, CartId = 1, ProductId = 1, Quantity = 2 };
            var cartItemSet = CreateMockDbSet(new List<CartItem> { cartItem }.AsQueryable());

            _contextMock.Setup(c => c.CartItems).Returns(cartItemSet.Object);

            // Act
            var result = await _cartItemService.GetCartItemById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
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
