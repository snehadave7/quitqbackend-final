using CaseStudyQuitQ.Exceptions;
using CaseStudyQuitQ.Models;
using CaseStudyQuitQ.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudyQuitQ.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase {
        private IPaymentService _service;
        public PaymentsController(IPaymentService service) {
            _service = service;
        }


        [Authorize(Roles = "Seller")]
        [HttpGet]
        public async Task<IActionResult> GetAllPayment() {
            try {
                List<Payment> payment = await _service.GetAllPayments();
                if (payment == null) throw new NullObjectException("No payment was found");
                return Ok(payment);
            }

            catch (NullObjectException e) {
                return NotFound(e.Message);
            }
        }

        [Authorize(Roles = "Seller,Customer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentById(int id) {
            try {
                if (id == 0) throw new InvalidIdException("Id cannot be 0");
                Payment payment = await _service.GetPaymentById(id);
                if (payment == null) throw new NullObjectException("No payment was found with id: " + id);
                
                return Ok(payment);
            }
            catch (InvalidIdException e) {
                return BadRequest(e.Message);
            }
            catch (NullObjectException e) {
                return NotFound(e.Message);
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> Post(Payment payment) {
            
            int result = await _service.AddNewPayment(payment);


            return Ok(result);
        }

        [Authorize(Roles = "Customer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Payment payment) {
            try {
                if (id == 0) throw new InvalidIdException("Id cannot be 0");
                if (id != payment.Id) throw new InvalidIdException("Id should be same as paymentId");
                Payment result = await _service.UpdatePayment(payment);
                if (result == null) throw new NullObjectException("No payment was found to update");
                return Ok(result);
            }
            catch (InvalidIdException e) {
                return BadRequest(e.Message);
            }
            catch (NullObjectException e) {
                return NotFound(e.Message);
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id) {
            try {
                if (id == 0) throw new InvalidIdException("Id cannot be 0");
                string result = await _service.DeletePayment(id);
                if (result == null) throw new NullObjectException("No Payment was found to delete");
                return Ok(result);
            }
            catch (InvalidIdException e) {
                return BadRequest(e.Message);
            }
            catch (NullObjectException e) {
                return NotFound(e.Message);
            }
        }
    }
}
