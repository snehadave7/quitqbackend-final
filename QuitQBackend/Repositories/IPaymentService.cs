using QuitQBackend.Models;

namespace QuitQBackend.Repositories {
    public interface IPaymentService {
        List<Payment> GetAllPayments();
        Payment GetPaymentById(int id);
        int AddNewPayment(Payment payment);
        Payment UpdatePayment(Payment payment);
        string DeletePayment(int id);
    }
}
