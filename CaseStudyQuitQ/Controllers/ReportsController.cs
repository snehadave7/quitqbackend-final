using CaseStudyQuitQ.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudyQuitQ.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase {
        private IReportService _service;
        public ReportsController(IReportService service) {
            _service = service;
        }

        // 1. Get Top Sellers Report
        [HttpGet("top-sellers")]
        public async Task<IActionResult> GetTopSellersReport() {
            var topSellers = await _service.GetTopSellersReportAsync();
            return Ok(topSellers);
        }

        // 2. Get Product Revenue Report
        [HttpGet("product-revenue")]
        public async Task<IActionResult> GetProductRevenueReport() {
            var productRevenue = await _service.GetProductRevenueReportAsync();
            return Ok(productRevenue);
        }

        // 3. Get Category Revenue Report
        [HttpGet("category-revenue")]
        public async Task<IActionResult> GetCategoryRevenueReport() {
            var categoryRevenue = await _service.GetCategoryRevenueReportAsync();
            return Ok(categoryRevenue);
        }

        // 4. Get Subcategory Revenue Report
        [HttpGet("subcategory-revenue")]
        public async Task<IActionResult> GetSubCategoryRevenueReport() {
            var subcategoryRevenue = await _service.GetSubCategoryRevenueReportAsync();
            return Ok(subcategoryRevenue);
        }
    }
}

