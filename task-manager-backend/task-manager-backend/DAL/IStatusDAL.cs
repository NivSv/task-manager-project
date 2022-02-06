using TaskManagerBackend.Models;

namespace TaskManagerBackend.DAL
{
    public interface IStatusDAL
    {
        List<Status> GetStatuses();
    }
}
