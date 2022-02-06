using TaskManagerBackend.Models;

namespace TaskManagerBackend.BL
{
    public interface IStatusBL
    {
        List<Status> GetStatuses();
    }
}
