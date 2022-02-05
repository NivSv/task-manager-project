using TaskManagerBackend.Models;

namespace TaskManagerBackend.BL
{
    public interface IStatusBL
    {
        void Create(StatusClientInfo status);
        void Delete(int statusID);
        List<Status> GetStatuses();
    }
}
