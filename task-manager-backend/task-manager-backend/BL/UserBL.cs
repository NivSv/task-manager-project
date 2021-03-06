
using TaskManagerBackend.DAL;
using TaskManagerBackend.Exceptions;
using TaskManagerBackend.Models;
using TaskManagerBackend.Utilities;

namespace TaskManagerBackend.BL
{
    public class UserBL : IUserBL
    {
        private readonly IUserDAL _userDAL;
        private readonly IAccessKeyManager _accessKeyManager;
        public UserBL(IUserDAL userDAL, IAccessKeyManager accessKeyValidator)
        {
            _userDAL = userDAL;
            _accessKeyManager = accessKeyValidator;
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
            if(Utility.ComputeSha256Hash(registerInfo.Password) != _userDAL.GetPasswordHash(registerInfo.Username)) throw new InvalidPasswordException("User/Password are wrong.");
            var accessKey = _accessKeyManager.GetAccessKey(_userDAL.GetByUsername(registerInfo.Username).UserId);
            if(accessKey != null) return accessKey;
            return _accessKeyManager.CreateAccessKey(_userDAL.GetByUsername(registerInfo.Username).UserId);
        }

        public void Register(RegisterInfo registerInfo)
        {
            _userDAL.Register(registerInfo);
        }
    }
}
