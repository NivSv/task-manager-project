using TaskManagerBackend.Models;

namespace TaskManagerBackend.BL
{
    public interface IUserBL
    {
        string Login(RegisterInfo registerInfo);
        void Register(RegisterInfo registerInfo);
        List<UserInfo> GetAll();
        User? GetById(int id);
        UserInfo? GetByUsername(string username);
    }
}
