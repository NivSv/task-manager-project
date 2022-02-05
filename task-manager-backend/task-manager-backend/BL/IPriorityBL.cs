using TaskManagerBackend.Models;

namespace TaskManagerBackend.BL
{
    public interface IPriorityBL
    {
        List<Priority> GetPriorities();
    }
}