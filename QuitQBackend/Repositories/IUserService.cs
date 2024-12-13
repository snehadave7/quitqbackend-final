using QuitQBackend.Models;

namespace QuitQBackend.Repositories {
    public interface IUserService {
        List<User> GetAllUsers();
        User GetUserById(int id);
        //int AddNewUser(User user);
        User UpdateUser(User user);
        string DeleteUser(int id);
    }
}
