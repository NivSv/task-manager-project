using TaskManagerBackend.Models;

namespace Oversight_Project.DAL
{
    public interface IStatusDAL
    {
        void Create(Status status);
        void Delete(int statusID);
        void Edit(Status status);
        List<Status> GetStatuses();
    }
}
