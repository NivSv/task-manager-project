using TaskManagerBackend.Models;

namespace TaskManagerBackend.DAL
{
    public class PriorityDAL : IPriorityDAL
    {
        public List<Priority> GetPriorities()
        { 
            using var context = new TaskManagerContext();
            var priorities = context.Priorities
            .ToList();
            return priorities;
        }
    }
}
