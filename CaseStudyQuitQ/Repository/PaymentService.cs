using CaseStudyQuitQ.Data;
using CaseStudyQuitQ.Models;

namespace CaseStudyQuitQ.Repository {
    public class PaymentService:IPaymentService {
        private readonly QuitQEcomContext _context;

        public PaymentService(QuitQEcomContext context) {
            _context = context;
        }
        public async Task<int> AddNewPayment(Payment payment) {
            if (payment != null) {
                _context.Payments.Add(payment);
                _context.SaveChanges();
                return payment.Id;
            }
            return 0;
        }

        public async Task<string> DeletePayment(int id) {
            if (id != null) {
                var Payment = _context.Payments.FirstOrDefault(x => x.Id == id);
                if (Payment != null) {
                    _context.Payments.Remove(Payment);
                    _context.SaveChanges();
                    return "The given Payment Id " + id + " is Removed";
                }
                else return null;
            }
            return null;
        }

        public async Task<List<Payment>> GetAllPayments() {
            var Payment = _context.Payments.ToList();
            if (Payment.Count > 0) return Payment;
            return null;
        }

        public async Task<Payment> GetPaymentById(int id) {
            if (id != 0 || id != null) {
                var Payment = _context.Payments.FirstOrDefault(x => x.Id == id);
                if (Payment != null) return Payment;
                else return null;
            }
            return null;
        }

        public async Task<Payment> UpdatePayment(Payment payment) {
            var existingPayment = _context.Payments.FirstOrDefault(x => x.Id == payment.Id);
            if (existingPayment != null) {
                existingPayment.OrderId = payment.OrderId;
                existingPayment.Status = payment.Status;
                existingPayment.Method = payment.Method;
                existingPayment.PaymentDate = payment.PaymentDate;
                _context.Entry(existingPayment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return payment;

            }
            return null;
        }
    }
}
