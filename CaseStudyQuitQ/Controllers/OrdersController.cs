using CaseStudyQuitQ.Exceptions;
using CaseStudyQuitQ.Models;
using CaseStudyQuitQ.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudyQuitQ.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase {
        private IOrderService _service;
        public OrdersController(IOrderService service) {
            _service = service;
        }

        //[Authorize(Roles = "Seller")]
        //[HttpGet]
        //public async Task<IActionResult> GetAllOrder() {
        //    try {
        //        List<Order> order = await _service.GetAllOrders();
        //        if (order== null) throw new NullObjectException("No order was found");
        //        return Ok(order);
        //    }
            
        //    catch (NullObjectException e) {
        //        return NotFound(e.Message);
        //    }

        //}


        [Authorize(Roles = "Seller,Customer")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id) {
            try {
                if (id == 0) throw new InvalidIdException("Id cannot be 0");
                Order order = await _service.GetOrderById(id);
                if (order == null) throw new NullObjectException("No order was found with id: " + id);
               
                return Ok(order);
            }
            catch (InvalidIdException e) {
                return BadRequest(e.Message);
            }
            catch (NullObjectException e) {
                return NotFound(e.Message);
            }

        }
        //[Authorize(Roles ="Customer")]
        [HttpGet]
        public async Task<IActionResult> GetOrderByUserId([FromQuery] int id) {
            try {
                if (id == 0) throw new InvalidIdException("Id cannot be 0");
                var order = await _service.GetOrderByUserId(id);
                if (order == null) throw new NullObjectException("No order was found with id: " + id);

                return Ok(order);
            }
            catch (InvalidIdException e) {
                return BadRequest(e.Message);
            }
            catch (NullObjectException e) {
                return NotFound(e.Message);
            }

        }
        //[Authorize(Roles = "Seller")]
        [HttpGet("bySellerId")]
        public async Task<IActionResult> GetOrderBySellerId([FromQuery] int sellerid) {
            try {
                if (sellerid == 0) throw new InvalidIdException("Id cannot be 0");
                var order = await _service.GetOrderBySellerId(sellerid);
                if (order == null) throw new NullObjectException("No order was found with id: " + sellerid);

                return Ok(order);
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
        public async Task<IActionResult> Post(Order order) {
            
            int result = await _service.AddNewOrder(order);


            return Ok(result);
        }

        [Authorize(Roles = "Seller")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Order order) {
            try {
                if (id == 0) throw new InvalidIdException("Id cannot be 0");
                if (id != order.Id) throw new InvalidIdException("Id should be same as orderId");
                Order result = await _service.UpdateOrder(order);
                if (result == null) throw new NullObjectException("No order was found to update");
                return Ok(result);
            }
            catch (InvalidIdException e) {
                return BadRequest(e.Message);
            }
            catch (NullObjectException e) {
                return NotFound(e.Message);
            }

        }

        [Authorize(Roles = "Seller")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id) {
            try {
                if (id == 0) throw new InvalidIdException("Id cannot be 0");
                string result = await _service.DeleteOrder(id);
                if (result == null) throw new NullObjectException("No Order was found to delete");
                return Ok(result);
            }
            catch (InvalidIdException e) {
                return BadRequest(e.Message);
            }
            catch (NullObjectException e) {
                return NotFound(e.Message);
            }

        }

        [HttpGet("SalesReport")]
        public async Task<ActionResult<List<object>>> GetSalesReportForSeller(int sellerId) {
            var salesReport = await _service.GetSalesReportForSellerAsync(sellerId);

            if (salesReport == null || salesReport.Count == 0) {
                return NotFound($"No sales report found for SellerId: {sellerId}");
            }

            return Ok(salesReport);
        }
    }
}
