using QuitQBackend.Data;
using QuitQBackend.Models;

namespace QuitQBackend.Repositories {
    public class PaymentService:IPaymentService {
        private readonly QuitQContext _context;

        public PaymentService(QuitQContext context) {
            _context = context;
        }
        public int AddNewPayment(Payment payment) {
            if (payment != null) {
                _context.Payments.Add(payment);
                _context.SaveChanges();
                return payment.Id;
            }
            return 0;
        }

        public string DeletePayment(int id) {
            if (id != null) {
                var Payment = _context.Payments.FirstOrDefault(x => x.Id == id);
                if (Payment != null) {
                    _context.Payments.Remove(Payment);
                    _context.SaveChanges();
                    return "The given Payment Id " + id + " is Removed";
                }
                else return "Something went wrong with deletion";
            }
            return null;
        }

        public List<Payment> GetAllPayments() {
            var Payment = _context.Payments.ToList();
            if (Payment.Count > 0) return Payment;
            return null;
        }

        public Payment GetPaymentById(int id) {
            if (id != 0 || id != null) {
                var Payment = _context.Payments.FirstOrDefault(x => x.Id == id);
                if (Payment != null) return Payment;
                else return null;
            }
            return null;
        }

        public Payment UpdatePayment(Payment payment) {
            var existingPayment = _context.Payments.FirstOrDefault(x => x.Id == payment.Id);
            if (existingPayment != null) {
                existingPayment.OrderId = payment.OrderId;
                existingPayment.Status = payment.Status;
                existingPayment.Method = payment.Method;
                existingPayment.PaymentDate = payment.PaymentDate;
                _context.Entry(existingPayment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();


            }
            return payment;
        }
    }
}
