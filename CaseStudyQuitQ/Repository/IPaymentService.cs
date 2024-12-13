using CaseStudyQuitQ.Models;

namespace CaseStudyQuitQ.Repository {
    public interface IPaymentService {
        Task<List<Payment>> GetAllPayments();
        Task<Payment> GetPaymentById(int id);
        Task<int> AddNewPayment(Payment payment);
        Task<Payment> UpdatePayment(Payment payment);
        Task<string >DeletePayment(int id);
    }
}
