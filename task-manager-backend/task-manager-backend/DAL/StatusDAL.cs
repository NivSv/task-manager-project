using TaskManagerBackend.Models;

namespace TaskManagerBackend.DAL
{
    public class StatusDAL : IStatusDAL
    {
        public List<Status> GetStatuses()
        {
            using var context = new TaskManagerContext();
            var statuses = context.Statuses
            .ToList();
            return statuses;
        }
    }
}
