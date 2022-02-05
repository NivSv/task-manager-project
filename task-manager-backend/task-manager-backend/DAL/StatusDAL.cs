using TaskManagerBackend.Models;

namespace TaskManagerBackend.DAL
{
    public class StatusDAL : IStatusDAL
    {
        public void Create(StatusClientInfo status)
        {
            using var context = new TaskManagerContext();
            var newStatus = new Status(){
                StatusName = status.StatusName,
            };
            context.Statuses.Add(newStatus);
            context.SaveChanges();
        }

        public void Delete(int statusID)
        {
            using var context = new TaskManagerContext();
            var entity = context.Statuses.FirstOrDefault(item => item.StatusId == statusID);
            if (entity != null)
            {
                context.Statuses.Remove(entity);
                context.SaveChanges();
            }
        }

        public List<Status> GetStatuses()
        {
            using var context = new TaskManagerContext();
            var statuses = context.Statuses
            .ToList();
            return statuses;
        }
    }
}
