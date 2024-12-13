using CaseStudyQuitQ.Data;
using CaseStudyQuitQ.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseStudyQuitQ.Repository {
    public class SubCategoryService:ISubCategoryService {
        private readonly QuitQEcomContext _context;

        public SubCategoryService(QuitQEcomContext context) {
            _context = context;
        }


        public async Task<int> AddNewSubCategory(SubCategory subCategory) {
            if (subCategory != null) {
                _context.SubCategories.Add(subCategory);
                _context.SaveChanges();
                return subCategory.Id;
            }
            return 0;
        }

        public async Task<string> DeleteSubCategory(int id) {
            if (id!=0) {
                var subCategory = _context.SubCategories.FirstOrDefault(x => x.Id == id);
                if (subCategory != null) {
                    _context.SubCategories.Remove(subCategory);
                    _context.SaveChanges();
                    return "The given SubCategory Id " + id + " is Removed";
                }
                else return null;
            }
            return "Id cannot be 0 or null";
        }

        public async Task<List<SubCategory>> GetAllSubCategorys() {
            var subCategory = _context.SubCategories.ToList();

            if (subCategory.Count > 0) return subCategory;
            return null;
        }

        public async Task<List<SubCategory>> GetSubCategoryByCatId(int catId) {

            var result = _context.SubCategories.AsQueryable();
            if(catId!= 0) {
                result=result.Where(x => x.CategoryId == catId);
            }
            return await result.ToListAsync();

        }

        public async Task<SubCategory> UpdateSubCategory(SubCategory subCategory) {
            var existingSubCategory =  _context.SubCategories.FirstOrDefault(x => x.Id == subCategory.Id);
            if (existingSubCategory != null) {
                existingSubCategory.Name = subCategory.Name;
                existingSubCategory.CategoryId = subCategory.CategoryId;
                //_context.Entry(existingSubCategory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return subCategory;

            }
            return null;

        }
    }
}
