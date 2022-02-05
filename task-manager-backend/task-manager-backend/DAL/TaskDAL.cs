using TaskManagerBackend.Exceptions;
using TaskManagerBackend.Models;

namespace TaskManagerBackend.DAL
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
            if (entity == null) throw new TaskNotExistsException("Task with ID "+ taskID+" is not exists.");
            context.Tasks.Remove(entity);
            context.SaveChanges();
        }

        public void Edit(Models.Task task)
        {
            using var context = new TaskManagerContext();
            var entity = context.Tasks.FirstOrDefault(item => item.TaskId == task.TaskId);
            if (entity == null) throw new TaskNotExistsException("Task with ID " + task.TaskId + " is not exists.");
            entity.TaskTitle = task.TaskTitle;
            entity.TaskDescription = task.TaskDescription;
            entity.TaskStatus = task.TaskStatus;
            entity.TaskPriority = task.TaskPriority;
            entity.TaskDeadline = task.TaskDeadline;
            entity.Assignee = task.Assignee;
            context.Tasks.Update(entity);
            context.SaveChanges();
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
            .Where(item => item.TaskPriority == priorityID)
            .ToList();
            return tasks;
        }

        public List<Models.Task> GetTasksByStatus(int statusID)
        {
            using var context = new TaskManagerContext();
            var tasks = context.Tasks
            .Where(item => item.TaskStatus == statusID)
            .ToList();
            return tasks;
        }

        public List<Models.Task> GetTasksByDeadline(DateTime date)
        {
            if (date < DateTime.UtcNow) throw new InvalidDateException("The date is earlier than today");
            using var context = new TaskManagerContext();
            var tasks = context.Tasks
            .Where(item => item.TaskDeadline <= date)
            .ToList();
            return tasks;
        }
    }
}
