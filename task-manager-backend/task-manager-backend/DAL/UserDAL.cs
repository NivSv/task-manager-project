using TaskManagerBackend.Models;
using TaskManagerBackend.Utilities;

namespace TaskManagerBackend.DAL
{
    public class UserDAL : IUserDAL
    {
        public List<User> GetAll()
        {
            using var context = new TaskManagerContext();
            var users = context.Users
            .ToList();
            return users;
        }

        public User? GetById(int id)
        {
            using var context = new TaskManagerContext();
            var user = context.Users.FirstOrDefault(item => item.UserId == id);
            return user;
        }

        public User? GetByUsername(string username)
        {
            using var context = new TaskManagerContext();
            var user = context.Users.FirstOrDefault(item => item.Username == username);
            return user;
        }

        public void Register(RegisterInfo registerInfo)
        {
            using var context = new TaskManagerContext();
            var user = new User();
            user.Username = registerInfo.Username;
            user.Password = Utility.ComputeSha256Hash(registerInfo.Password);
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
