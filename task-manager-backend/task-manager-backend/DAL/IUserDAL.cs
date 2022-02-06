using TaskManagerBackend.Models;

namespace TaskManagerBackend.DAL
{
    public interface IUserDAL
    {
        void Register(RegisterInfo registerInfo); 
        List<User> GetAll();
        User? GetById(int id);
        User? GetByUsername(string username);
        string GetPasswordHash(string username);
    }
}
