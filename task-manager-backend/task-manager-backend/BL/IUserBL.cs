using Oversight_Project.Models;

namespace Oversight_Project.BL
{
    public interface IUserBL
    {
        string Login(RegisterInfo registerInfo);
        void Register(RegisterInfo registerInfo);
        List<User> GetAll();
        User? GetById(int id);
        User? GetByUsername(string username);
    }
}
