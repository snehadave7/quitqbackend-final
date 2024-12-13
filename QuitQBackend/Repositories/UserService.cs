using QuitQBackend.Data;
using QuitQBackend.Models;

namespace QuitQBackend.Repositories {
    public class UserService : IUserService {
        private readonly QuitQContext _context;

        public UserService(QuitQContext context) {
            _context = context;
        }


        //public int AddNewUser(User user) {
        //    if (user != null) {
        //        _context.Users.Add(user);
        //        _context.SaveChanges();
        //        return user.Id;
        //    }
        //    return 0;
        //}

        public string DeleteUser(int id) {
            if (id != null) {
                var user = _context.Users.FirstOrDefault(x => x.Id == id);
                if (user != null) {
                    _context.Users.Remove(user);
                    _context.SaveChanges();
                    return "The given User Id " + id + " is Removed";
                }
                else return "Something went wrong with deletion";
            }
            return null;
        }

        public List<User> GetAllUsers() {
            var users = _context.Users.ToList();
            if(users.Count > 0) return users;
            return null;
        }

        public User GetUserById(int id) {

            if (id != 0 || id!=null) {
                var user= _context.Users.FirstOrDefault(x=>x.Id == id);
                if (user != null) return user;
                else return null;
            }
            return null;
        }

        public User UpdateUser(User user) {
            var existingUser=_context.Users.FirstOrDefault(x=>x.Id==user.Id);
            if (existingUser != null) {
                existingUser.FullName = user.FullName;
                existingUser.ContactNumber = user.ContactNumber;
                existingUser.Password=user.Password;
                existingUser.Email = user.Email;
                existingUser.Role = user.Role;
                _context.Entry(existingUser).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                

            }
            return user;

        }
    }
}
