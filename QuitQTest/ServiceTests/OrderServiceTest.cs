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
    internal class OrderServiceTest {
        private Mock<QuitQEcomContext> _contextMock;
        private OrderService _orderService;

        [SetUp]
        public void Setup() {
            _contextMock = new Mock<QuitQEcomContext>();
            _orderService = new OrderService(_contextMock.Object);
        }

        
        [Test]
        public async Task AddNewOrder_InsufficientStock_ReturnsMinusOne() {
            // Arrange
            var product = new Product { Id = 1, Stock = 1 };
            var order = new Order { Id = 1, ProductId = 1, Quantity = 5 };

            var productSet = CreateMockDbSet(new List<Product> { product }.AsQueryable());
            _contextMock.Setup(c => c.Products).Returns(productSet.Object);

            // Act
            var result = await _orderService.AddNewOrder(order);

            // Assert
            Assert.AreEqual(-1, result);
            _contextMock.Verify(c => c.SaveChanges(), Times.Never);
        }

        [Test]
        public async Task DeleteOrder_ValidOrder_ReturnsSuccessMessage() {
            // Arrange
            var order = new Order { Id = 1, ProductId = 1, Quantity = 2 };
            var product = new Product { Id = 1, Stock = 8 };

            var orderSet = CreateMockDbSet(new List<Order> { order }.AsQueryable());
            var productSet = CreateMockDbSet(new List<Product> { product }.AsQueryable());

            _contextMock.Setup(c => c.Orders).Returns(orderSet.Object);
            _contextMock.Setup(c => c.Products).Returns(productSet.Object);

            // Act
            var result = await _orderService.DeleteOrder(1);

            // Assert
            Assert.AreEqual("The given Order Id 1 is Removed", result);
            Assert.AreEqual(10, product.Stock); // Stock restored
            orderSet.Verify(m => m.Remove(It.Is<Order>(o => o.Id == 1)), Times.Once);
            _contextMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Test]
        public async Task GetAllOrders_ReturnsOrdersWithProductDetails() {
            // Arrange
            var orders = new List<Order>
            {
                new Order { Id = 1, ProductId = 1, Quantity = 2 },
                new Order { Id = 2, ProductId = 2, Quantity = 1 }
            }.AsQueryable();

            var orderSet = CreateMockDbSet(orders);
            _contextMock.Setup(c => c.Orders).Returns(orderSet.Object);

            // Act
            var result = await _orderService.GetAllOrders();

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




