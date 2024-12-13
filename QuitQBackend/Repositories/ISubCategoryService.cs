using QuitQBackend.Models;

namespace QuitQBackend.Repositories {
    public interface ISubCategoryService {
        List<SubCategory> GetAllSubCategorys();
        SubCategory GetSubCategoryById(int id);
        int AddNewSubCategory(SubCategory SubCategory);
        SubCategory UpdateSubCategory(SubCategory SubCategory);
        string DeleteSubCategory(int id);
    }
}
