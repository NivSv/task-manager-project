using TaskManagerBackend.Models;
using TaskManagerBackend.DAL;
using TaskManagerBackend.Exceptions;

namespace TaskManagerBackend.BL
{
    public class TaskBL : ITaskBL
    {
        private readonly ITaskDAL _taskDAL;
        private readonly IStatusDAL _statusDAL;
        private readonly IPriorityDAL _priorityDAL;
        private readonly IUserDAL _userDAL;
        public TaskBL(ITaskDAL taskDAL, IStatusDAL statusDAL, IPriorityDAL priorityDAL, IUserDAL userDAL)
        {
            _taskDAL = taskDAL;
            _statusDAL = statusDAL;
            _priorityDAL = priorityDAL;
            _userDAL = userDAL;
        }
        public void Create(TaskCreateInfo task)
        {
            var priority = _priorityDAL.GetPriorities().Find(status => status.PriorityName.ToLower() == task.TaskPriority.ToLower());
            if (priority == null) throw new InvalidPriorityException("Priority " + task.TaskPriority + " is not exist.");
            var user = _userDAL.GetAll().Find(status => status.Username.ToLower() == task.Assignee.ToLower());
            if (user == null) throw new UserNotExistsException("User " + task.Assignee + " is not exist.");
            var newtask = new Models.Task()
            {
                TaskTitle = task.TaskTitle,
                TaskDescription = task.TaskDescription,
                TaskPriority = priority.PriorityID,
                TaskCreatedDate = DateTime.UtcNow,
                TaskDeadline = task.TaskDeadline,
                Assignee = user.UserId,
                TaskStatus = 1,
            };
            _taskDAL.Create(newtask);
        }

        public void Delete(int taskID)
        {
            _taskDAL.Delete(taskID);
        }

        public void Edit(Models.Task task)
        {
            _taskDAL.Edit(task);
        }

        public List<Models.Task> GetAllTasks()
        {
            return _taskDAL.GetAllTasks();
        }

        public List<Models.Task> GetTaskByUserID(int userID)
        {
            return _taskDAL.GetTaskByUserID(userID);
        }

        public List<Models.Task> GetTasksByPriority(string priorityName)
        {
            var entity = _priorityDAL.GetPriorities().Find(status => status.PriorityName.ToLower() == priorityName.ToLower());
            if (entity == null) throw new InvalidPriorityException("Priority " + priorityName + " is not exist.");
            return _taskDAL.GetTasksByPriority(entity.PriorityID);
        }

        public List<Models.Task> GetTasksByStatus(string statusName)
        {
            var entity = _statusDAL.GetStatuses().Find(status => status.StatusName.ToLower() == statusName.ToLower());
            if (entity == null) throw new InvalidStatusException("Status "+statusName+" is not exist.");
            return _taskDAL.GetTasksByStatus(entity.StatusId);
        }

        public List<Models.Task> GetTasksByDeadline(string date)
        {
            var newdate = DateTime.Parse(date);
            return _taskDAL.GetTasksByDeadline(newdate);
        }
    }
}
