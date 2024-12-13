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
    internal class DeliveryAddressServiceTest {

        private Mock<QuitQEcomContext> _contextMock;
        private DeliveryAddressService _deliveryAddressService;

        [SetUp]
        public void Setup() {
            _contextMock = new Mock<QuitQEcomContext>();
            _deliveryAddressService = new DeliveryAddressService(_contextMock.Object);
        }

        [Test]
        public async Task AddNewDeliveryAddress_ValidDeliveryAddress_ReturnsId() {
            // Arrange
            var deliveryAddress = new DeliveryAddress { Id = 1, UserId = 1, Address = "123 Street", City = "City", Pincode = "123456" };
            var deliveryAddressSet = CreateMockDbSet(new List<DeliveryAddress>().AsQueryable());

            _contextMock.Setup(c => c.DeliveryAddresses).Returns(deliveryAddressSet.Object);

            // Act
            var result = await _deliveryAddressService.AddNewDeliveryAddress(deliveryAddress);

            // Assert
            Assert.AreEqual(1, result);
            deliveryAddressSet.Verify(m => m.Add(It.Is<DeliveryAddress>(d => d.Id == 1)), Times.Once);
            _contextMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Test]
        public async Task DeleteDeliveryAddress_ValidId_ReturnsSuccessMessage() {
            // Arrange
            var deliveryAddress = new DeliveryAddress { Id = 1, UserId = 1, Address = "123 Street" };
            var deliveryAddressSet = CreateMockDbSet(new List<DeliveryAddress> { deliveryAddress }.AsQueryable());

            _contextMock.Setup(c => c.DeliveryAddresses).Returns(deliveryAddressSet.Object);

            // Act
            var result = await _deliveryAddressService.DeleteDeliveryAddress(1);

            // Assert
            Assert.AreEqual("The given DeliveryAddress Id 1 is Removed", result);
            deliveryAddressSet.Verify(m => m.Remove(It.Is<DeliveryAddress>(d => d.Id == 1)), Times.Once);
            _contextMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Test]
        public async Task GetAllDeliveryAddresses_ValidUserId_ReturnsDeliveryAddresses() {
            // Arrange
            var deliveryAddresses = new List<DeliveryAddress>
            {
                new DeliveryAddress { Id = 1, UserId = 1, Address = "123 Street" },
                new DeliveryAddress { Id = 2, UserId = 1, Address = "456 Avenue" }
            }.AsQueryable();

            var deliveryAddressSet = CreateMockDbSet(deliveryAddresses);
            _contextMock.Setup(c => c.DeliveryAddresses).Returns(deliveryAddressSet.Object);

            // Act
            var result = await _deliveryAddressService.GetAllDeliveryAddresss(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public async Task GetDeliveryAddressById_ValidId_ReturnsDeliveryAddress() {
            // Arrange
            var deliveryAddress = new DeliveryAddress { Id = 1, UserId = 1, Address = "123 Street" };
            var deliveryAddressSet = CreateMockDbSet(new List<DeliveryAddress> { deliveryAddress }.AsQueryable());

            _contextMock.Setup(c => c.DeliveryAddresses).Returns(deliveryAddressSet.Object);

            // Act
            var result = await _deliveryAddressService.GetDeliveryAddressById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("123 Street", result.Address);
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



