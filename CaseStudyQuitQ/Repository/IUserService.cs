using CaseStudyQuitQ.Models;

namespace CaseStudyQuitQ.Repository {
    public interface IUserService {
        Task<List<User> >GetAllUsers(); // seen by admin
        Task<User> GetUserById(int id); 
        Task<User> UpdateUser(User user);
        Task<string> DeleteUser(int id);
        Task<User> GetUserByUsername(string username);
        Task<List<User>> GetAllCustomer();
        Task<List<User>> GetAllSeller();
    }
}
