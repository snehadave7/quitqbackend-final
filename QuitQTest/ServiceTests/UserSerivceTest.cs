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
    internal class UserSerivceTest {
        private Mock<QuitQEcomContext> _contextMock;
        private UserService _userService;

        [SetUp]
        public void Setup() {
            _contextMock = new Mock<QuitQEcomContext>();
            _userService = new UserService(_contextMock.Object);
        }

        [Test]
        public async Task DeleteUser_UserExists_ReturnsSuccessMessage() {
            // Arrange
            var user = new User { Id = 1, UserName = "testuser" };
            var users = new List<User> { user }.AsQueryable();
            var mockSet = CreateMockDbSet(users);

            _contextMock.Setup(c => c.Users).Returns(mockSet.Object);

            // Act
            var result = await _userService.DeleteUser(1);

            // Assert
            Assert.AreEqual("The given User Id 1 is Removed", result);
            mockSet.Verify(m => m.Remove(It.Is<User>(u => u.Id == 1)), Times.Once);
            _contextMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Test]
        public async Task DeleteUser_UserDoesNotExist_ReturnsNull() {
            // Arrange
            var users = new List<User>().AsQueryable();
            var mockSet = CreateMockDbSet(users);

            _contextMock.Setup(c => c.Users).Returns(mockSet.Object);

            // Act
            var result = await _userService.DeleteUser(1);

            // Assert
            Assert.IsNull(result);
            mockSet.Verify(m => m.Remove(It.IsAny<User>()), Times.Never);
            _contextMock.Verify(c => c.SaveChanges(), Times.Never);
        }

        [Test]
        public async Task GetAllUsers_ReturnsListOfUsers() {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, UserName = "user1" },
                new User { Id = 2, UserName = "user2" }
            }.AsQueryable();
            var mockSet = CreateMockDbSet(users);

            _contextMock.Setup(c => c.Users).Returns(mockSet.Object);

            // Act
            var result = await _userService.GetAllUsers();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public async Task GetAllCustomer_ReturnsOnlyCustomers() {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, UserName = "customer1", Role = "customer" },
                new User { Id = 2, UserName = "seller1", Role = "seller" }
            }.AsQueryable();
            var mockSet = CreateMockDbSet(users);

            _contextMock.Setup(c => c.Users).Returns(mockSet.Object);

            // Act
            var result = await _userService.GetAllCustomer();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("customer", result.First().Role);
        }

        [Test]
        public async Task GetUserById_UserExists_ReturnsUser() {
            // Arrange
            var user = new User { Id = 1, UserName = "testuser" };
            var users = new List<User> { user }.AsQueryable();
            var mockSet = CreateMockDbSet(users);

            _contextMock.Setup(c => c.Users).Returns(mockSet.Object);

            // Act
            var result = await _userService.GetUserById(1);

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
