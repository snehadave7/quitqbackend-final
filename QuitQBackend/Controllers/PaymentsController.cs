using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuitQBackend.Models;
using QuitQBackend.Repositories;

namespace QuitQBackend.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase {
        private IPaymentService _service;
        public PaymentsController(IPaymentService service) {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllPayment() {
            List<Payment> Payment = _service.GetAllPayments();

            return Ok(Payment);
        }
        [HttpGet("{id}")]
        public IActionResult GetPaymentById(int id) {
            Payment Payment = _service.GetPaymentById(id);

            if (Payment == null) {
                return NotFound();
            }
            return Ok(Payment);
        }
        [HttpPost]
        public IActionResult Post(Payment payment) {
            int result = _service.AddNewPayment(payment);


            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Payment payment) {
            if (id != payment.Id) return BadRequest();
            Payment result = _service.UpdatePayment(payment);
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(int id) {
            string result = _service.DeletePayment(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
