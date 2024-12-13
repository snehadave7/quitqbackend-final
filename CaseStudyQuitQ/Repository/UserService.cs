using CaseStudyQuitQ.Data;
using CaseStudyQuitQ.Models;

namespace CaseStudyQuitQ.Repository {
    public class UserService : IUserService {
        private readonly QuitQEcomContext _context;

        public UserService(QuitQEcomContext context) {
            _context = context;
        }


        public async Task<string> DeleteUser(int id) {
            if (id != 0) {
                var user = _context.Users.FirstOrDefault(x => x.Id == id);
                if (user != null) {
                    _context.Users.Remove(user);
                    _context.SaveChanges();
                    return "The given User Id " + id + " is Removed";
                }
                else return null;
            }
            return "Id cannot be 0 or null";

        }
       
        public async Task<List<User>> GetAllUsers() {
            var users = _context.Users.ToList();
            if(users.Count > 0) return users;
            return null;
        }
        public async Task<List<User>> GetAllCustomer() {
            var users = _context.Users.Where(x=>x.Role=="customer").ToList();
            if (users.Count > 0) return users;
            return null;
        }
        public async Task<List<User>> GetAllSeller() {
            var users = _context.Users.Where(x => x.Role == "seller").ToList();
            if (users.Count > 0) return users;
            return null;
        }


        public async Task<User> GetUserById(int id) {

            if (id != 0) {
                var user= _context.Users.FirstOrDefault(x=>x.Id == id);
                if (user != null) return user;
                else return null;
            }
            return null;
        }

        public async Task<User> GetUserByUsername(string username) {
            if (username!=null) {
                var user = _context.Users.FirstOrDefault(x =>x.UserName==username);
                if (user != null) return user;
                else return null;
            }
            return null;

        }

        public async Task<User> UpdateUser(User user) {
            var existingUser= _context.Users.FirstOrDefault(x=>x.Id==user.Id);
            if (existingUser != null) {
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.UserName = user.UserName;
                existingUser.ContactNumber = user.ContactNumber;
                existingUser.Password=user.Password;
                existingUser.Email = user.Email;
                existingUser.Role = user.Role;
                 _context.Entry(existingUser).State=Microsoft.EntityFrameworkCore.EntityState.Modified; // it marks existing user as modified to ensure Entity Framework tracks and saves the changes.
                _context.SaveChanges();
                return user;

            }
            return null;

        }
    }
}
