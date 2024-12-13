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
    internal class PaymentServiceTest {


        public class PaymentServiceTests {
            private Mock<QuitQEcomContext> _contextMock;
            private PaymentService _paymentService;

            [SetUp]
            public void Setup() {
                _contextMock = new Mock<QuitQEcomContext>();
                _paymentService = new PaymentService(_contextMock.Object);
            }

            [Test]
            public async Task AddNewPayment_ValidPayment_ReturnsPaymentId() {
                // Arrange
                var payment = new Payment { Id = 1, Status = "Completed", Method = "Credit Card" };
                var mockSet = CreateMockDbSet(new List<Payment>().AsQueryable());

                _contextMock.Setup(c => c.Payments).Returns(mockSet.Object);

                // Act
                var result = await _paymentService.AddNewPayment(payment);

                // Assert
                Assert.AreEqual(1, result);
                mockSet.Verify(m => m.Add(It.Is<Payment>(p => p.Id == 1)), Times.Once);
                _contextMock.Verify(c => c.SaveChanges(), Times.Once);
            }

            [Test]
            public async Task DeletePayment_PaymentExists_ReturnsSuccessMessage() {
                // Arrange
                var payment = new Payment { Id = 1, Status = "Pending" };
                var payments = new List<Payment> { payment }.AsQueryable();
                var mockSet = CreateMockDbSet(payments);

                _contextMock.Setup(c => c.Payments).Returns(mockSet.Object);

                // Act
                var result = await _paymentService.DeletePayment(1);

                // Assert
                Assert.AreEqual("The given Payment Id 1 is Removed", result);
                mockSet.Verify(m => m.Remove(It.Is<Payment>(p => p.Id == 1)), Times.Once);
                _contextMock.Verify(c => c.SaveChanges(), Times.Once);
            }

            [Test]
            public async Task GetAllPayments_ReturnsListOfPayments() {
                // Arrange
                var payments = new List<Payment>
            {
                new Payment { Id = 1, Status = "Completed" },
                new Payment { Id = 2, Status = "Pending" }
            }.AsQueryable();
                var mockSet = CreateMockDbSet(payments);

                _contextMock.Setup(c => c.Payments).Returns(mockSet.Object);

                // Act
                var result = await _paymentService.GetAllPayments();

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(2, result.Count);
            }

            [Test]
            public async Task GetPaymentById_PaymentExists_ReturnsPayment() {
                // Arrange
                var payment = new Payment { Id = 1, Status = "Completed" };
                var payments = new List<Payment> { payment }.AsQueryable();
                var mockSet = CreateMockDbSet(payments);

                _contextMock.Setup(c => c.Payments).Returns(mockSet.Object);

                // Act
                var result = await _paymentService.GetPaymentById(1);

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
}



