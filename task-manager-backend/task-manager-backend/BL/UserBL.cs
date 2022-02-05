
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

        public List<UserInfo> GetAll()
        {
            var users = _userDAL.GetAll().Select(x => new UserInfo()
            {
                Username = x.Username,
                UserId = x.UserId,
            }).ToList();
            return users;
        }

        public User? GetById(int id)
        {
            return _userDAL.GetById(id);
        }

        public UserInfo? GetByUsername(string username)
        {
            var olduser = _userDAL.GetByUsername(username);
            var newuser = new UserInfo()
            {
                Username = olduser.Username,
                UserId = olduser.UserId,
            };
            return newuser;
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
