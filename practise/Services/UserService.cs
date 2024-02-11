using practise.Database;
using practise.Entitys;

namespace practise.Services
{
    public class UserService : IUserService
    {
        private readonly BookContext Context = null;
        
        public UserService(BookContext context)
        {
            Context = context;
            
        }
        public void AddUser(User user)
        {
            try
            {
                if (user != null)
                {
                    Context.Users.Add(user);
                    Context.SaveChanges();
                  
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteUser(int userId)
        {
            try
            {
                User user = Context.Users.SingleOrDefault(u => u.UserId == userId);
                if (user != null)
                {
                    Context.Users.Remove(user);
                    Context.SaveChanges();

                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<User> GetUsers()
        {
            try
            {
                var Result = Context.Users.ToList();
                return Result;
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                Context.Users.Update(user);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public User ValidteUser(string email, string password)
        {
            return Context.Users.SingleOrDefault(u => u.Email == email && u.Password == password);
        }
    }
}
