using QuitQBackend.Data;
using QuitQBackend.Models;

namespace QuitQBackend.Repositories {
    public class SubCategoryService:ISubCategoryService {
        private readonly QuitQContext _context;

        public SubCategoryService(QuitQContext context) {
            _context = context;
        }


        public int AddNewSubCategory(SubCategory subCategory) {
            if (subCategory != null) {
                _context.SubCategories.Add(subCategory);
                _context.SaveChanges();
                return subCategory.Id;
            }
            return 0;
        }

        public string DeleteSubCategory(int id) {
            if (id != null) {
                var subCategory = _context.SubCategories.FirstOrDefault(x => x.Id == id);
                if (subCategory != null) {
                    _context.SubCategories.Remove(subCategory);
                    _context.SaveChanges();
                    return "The given SubCategory Id " + id + " is Removed";
                }
                else return "Something went wrong with deletion";
            }
            return null;
        }

        public List<SubCategory> GetAllSubCategorys() {
            var subCategory = _context.SubCategories.ToList();
            if (subCategory.Count > 0) return subCategory;
            return null;
        }

        public SubCategory GetSubCategoryById(int id) {

            if (id != 0 || id != null) {
                var subCategory = _context.SubCategories.FirstOrDefault(x => x.Id == id);
                if (subCategory != null) return subCategory;
                else return null;
            }
            return null;
        }

        public SubCategory UpdateSubCategory(SubCategory SubCategory) {
            var existingSubCategory = _context.SubCategories.FirstOrDefault(x => x.Id == SubCategory.Id);
            if (existingSubCategory != null) {
                existingSubCategory.Name = SubCategory.Name;
                existingSubCategory.CategoryId = SubCategory.CategoryId;
                _context.Entry(existingSubCategory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();


            }
            return SubCategory;

        }
    }
}
