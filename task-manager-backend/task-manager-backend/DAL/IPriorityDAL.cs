using TaskManagerBackend.Models;

namespace TaskManagerBackend.DAL
{
    public interface IPriorityDAL
    {
        List<Priority> GetPriorities();
    }
}
