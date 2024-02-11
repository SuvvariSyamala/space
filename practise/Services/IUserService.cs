using practise.Entitys;

namespace practise.Services
{
    public interface IUserService
    {
        void AddUser(User user);
        List<User> GetUsers();
      
        void UpdateUser(User user);
        void DeleteUser(int userId);
        User ValidteUser(string email, string password);
    }
}
