
using TaskManagerBackend.DAL;
using TaskManagerBackend.Models;

namespace TaskManagerBackend.BL
{
    public class UserBL : IUserBL
    {
        private readonly IUserDAL _userDAL;
        public UserBL(IUserDAL userDAL)
        {
            _userDAL = userDAL;
        }

        public List<User> GetAll()
        {
            return _userDAL.GetAll();
        }

        public User? GetById(int id)
        {
            return _userDAL.GetById(id);
        }

        public User? GetByUsername(string username)
        {
            return _userDAL.GetByUsername(username);
        }

        public string Login(RegisterInfo registerInfo)
        {
            throw new NotImplementedException();
        }

        public void Register(RegisterInfo registerInfo)
        {
            _userDAL.Register(registerInfo);
        }
    }
}
