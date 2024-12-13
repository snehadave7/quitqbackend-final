using CaseStudyQuitQ.Dto;

namespace CaseStudyQuitQ.Repository {
    public interface IReportService {
        Task<List<TopSellerReportDto>> GetTopSellersReportAsync();
        Task<List<ProductRevenueReportDto>> GetProductRevenueReportAsync();
        Task<List<CategoryRevenueReportDto>> GetCategoryRevenueReportAsync();
        Task<List<SubCategoryRevenueReportDto>> GetSubCategoryRevenueReportAsync();
    }
}
