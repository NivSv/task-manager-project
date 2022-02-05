using Oversight_Project.Models;

namespace Oversight_Project.DAL
{
    public class TaskDAL : ITaskDAL
    {
        public void Create(Models.Task task)
        {
            using var context = new TaskManagerContext();
            context.Tasks.Add(task);
            context.SaveChanges();
        }

        public void Delete(int taskID)
        {
            using var context = new TaskManagerContext();
            var entity = context.Tasks.FirstOrDefault(item => item.TaskId == taskID);
            if (entity != null)
            {
                context.Tasks.Remove(entity);
                context.SaveChanges();
            }
        }

        public void Edit(Models.Task task)
        {
            throw new NotImplementedException();
        }

        public List<Models.Task> GetAllTasks()
        {
            using var context = new TaskManagerContext();
            var tasks = context.Tasks
            .ToList();
            return tasks;
        }

        public List<Models.Task> GetTaskByUserID(int userID)
        {
            using var context = new TaskManagerContext();
            var tasks = context.Tasks
            .Where(item => item.Assignee == userID)
            .ToList();
            return tasks;
        }

        public List<Models.Task> GetTasksByPriority(int priorityID)
        {
            using var context = new TaskManagerContext();
            var tasks = context.Tasks
            .Where(item => item.TaskId == priorityID)
            .ToList();
            return tasks;
        }

        public List<Models.Task> GetTasksByStatus(int statusID)
        {
            using var context = new TaskManagerContext();
            var tasks = context.Tasks
            .Where(item => item.TaskId == statusID)
            .ToList();
            return tasks;
        }

        public List<Models.Task> WithinAWeek()
        {

        }
    }
}
