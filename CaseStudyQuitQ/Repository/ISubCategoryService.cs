using CaseStudyQuitQ.Models;

namespace CaseStudyQuitQ.Repository {
    public interface ISubCategoryService {
        Task<List<SubCategory> >GetAllSubCategorys();
        Task<List<SubCategory>> GetSubCategoryByCatId(int id);
        Task<int> AddNewSubCategory(SubCategory SubCategory);
        Task<SubCategory> UpdateSubCategory(SubCategory SubCategory);
        Task<string> DeleteSubCategory(int id);
    }
}
