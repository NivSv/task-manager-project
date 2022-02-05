using TaskManagerBackend.Exceptions;
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
            if (user == null) throw new UserNotExistsException("User with id" + id + " is not exists.");
            return user;
        }

        public User? GetByUsername(string username)
        {
            using var context = new TaskManagerContext();
            var user = context.Users.FirstOrDefault(item => item.Username == username);
            if (user == null) throw new UserNotExistsException("User " + username + " is not exists.");
            return user;
        }

        public void Register(RegisterInfo registerInfo)
        {
            using var context = new TaskManagerContext();
            var entity = context.Users.FirstOrDefault(item => item.Username == registerInfo.Username);
            if (entity != null) throw new UserAlreadyExistsException("User " + registerInfo.Username + " is already exists.");
            var user = new User()
            {
                Username = registerInfo.Username,
                Password = Utility.ComputeSha256Hash(registerInfo.Password),
            };
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
