using Oversight_Project.Models;

namespace Oversight_Project.DAL
{
    public interface IUserDAL
    {
        void Register(RegisterInfo registerInfo); 
        List<User> GetAll();
        User? GetById(int id);
        User? GetByUsername(string username);
    }
}
