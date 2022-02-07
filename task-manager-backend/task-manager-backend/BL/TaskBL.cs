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
        public void Create(TaskCreationInfo task)
        {
            var priority = _priorityDAL.GetPriorities().Find(x => x.PriorityName.ToLower() == task.TaskPriority.ToLower());
            if (priority == null) throw new InvalidPriorityException("Priority " + task.TaskPriority + " is not exist.");
            var user = _userDAL.GetAll().Find(x => x.Username.ToLower() == task.Assignee.ToLower());
            if (user == null) throw new UserNotExistsException("User " + task.Assignee + " is not exist.");
            var status = _statusDAL.GetStatuses().Find(x => x.StatusName.ToLower() == task.TaskStatus.ToLower());
            if (status == null) throw new InvalidStatusException("Status " + task.TaskStatus + " is not exist.");
            var newtask = new Models.Task()
            {
                TaskTitle = task.TaskTitle,
                TaskDescription = task.TaskDescription,
                TaskPriority = priority.PriorityID,
                TaskCreatedDate = DateTime.UtcNow,
                TaskDeadline = task.TaskDeadline,
                Assignee = user.UserId,
                TaskStatus = status.StatusId,
            };
            _taskDAL.Create(newtask);
        }

        public void Delete(int taskID)
        {
            _taskDAL.Delete(taskID);
        }

        public void Edit(TaskCreationInfo task,int taskID)
        {
            var priority = _priorityDAL.GetPriorities().Find(x => x.PriorityName.ToLower() == task.TaskPriority.ToLower());
            if (priority == null) throw new InvalidPriorityException("Priority " + task.TaskPriority + " is not exist.");
            var user = _userDAL.GetAll().Find(x => x.Username.ToLower() == task.Assignee.ToLower());
            if (user == null) throw new UserNotExistsException("User " + task.Assignee + " is not exist.");
            var status = _statusDAL.GetStatuses().Find(x => x.StatusName.ToLower() == task.TaskStatus.ToLower());
            if (status == null) throw new InvalidStatusException("Status " + task.TaskStatus + " is not exist.");
            var newtask = new Models.Task()
            {
                TaskId = taskID,
                TaskTitle = task.TaskTitle,
                TaskDescription = task.TaskDescription,
                TaskPriority = priority.PriorityID,
                TaskCreatedDate = DateTime.UtcNow,
                TaskDeadline = task.TaskDeadline,
                Assignee = user.UserId,
                TaskStatus = status.StatusId,
            };
            _taskDAL.Edit(newtask);
        }

        public List<Models.TaskInfo> GetAllTasks()
        {
            var priorities = _priorityDAL.GetPriorities();
            var statuses = _statusDAL.GetStatuses();
            var newTasks = _taskDAL.GetAllTasks().Select(x => new TaskInfo
            {
                TaskId = x.TaskId,
                TaskTitle = x.TaskTitle,
                TaskDescription = x.TaskDescription,
                TaskPriority = priorities.Find(priority => x.TaskPriority == priority.PriorityID).PriorityName,
                TaskCreatedDate =x.TaskCreatedDate,
                TaskDeadline = x.TaskDeadline,
                Assignee = _userDAL.GetById(x.Assignee).Username,
                TaskStatus = statuses.Find(status => x.TaskStatus == status.StatusId).StatusName,
            }).ToList();
            return newTasks;
        }

        public List<Models.TaskInfo> GetTaskByUserID(int userID)
        {
            var priorities = _priorityDAL.GetPriorities();
            var statuses = _statusDAL.GetStatuses();
            var newTasks = _taskDAL.GetTaskByUserID(userID).Select(x => new TaskInfo
            {
                TaskId = x.TaskId,
                TaskTitle = x.TaskTitle,
                TaskDescription = x.TaskDescription,
                TaskPriority = priorities.Find(priority => x.TaskPriority == priority.PriorityID).PriorityName,
                TaskCreatedDate = x.TaskCreatedDate,
                TaskDeadline = x.TaskDeadline,
                Assignee = _userDAL.GetById(x.Assignee).Username,
                TaskStatus = statuses.Find(status => x.TaskStatus == status.StatusId).StatusName,
            }).ToList();
            return newTasks;
        }

        public List<Models.TaskInfo> GetTasksByPriority(string priorityName)
        {
            var entity = _priorityDAL.GetPriorities().Find(status => status.PriorityName.ToLower() == priorityName.ToLower());
            if (entity == null) throw new InvalidPriorityException("Priority " + priorityName + " is not exist.");
            var priorities = _priorityDAL.GetPriorities();
            var statuses = _statusDAL.GetStatuses();
            var newTasks = _taskDAL.GetTasksByPriority(entity.PriorityID).Select(x => new TaskInfo
            {
                TaskId = x.TaskId,
                TaskTitle = x.TaskTitle,
                TaskDescription = x.TaskDescription,
                TaskPriority = priorities.Find(priority => x.TaskPriority == priority.PriorityID).PriorityName,
                TaskCreatedDate = x.TaskCreatedDate,
                TaskDeadline = x.TaskDeadline,
                Assignee = _userDAL.GetById(x.Assignee).Username,
                TaskStatus = statuses.Find(status => x.TaskStatus == status.StatusId).StatusName,
            }).ToList();
            return newTasks;
        }

        public List<Models.TaskInfo> GetTasksByStatus(string statusName)
        {
            var entity = _statusDAL.GetStatuses().Find(status => status.StatusName.ToLower() == statusName.ToLower());
            if (entity == null) throw new InvalidStatusException("Status "+statusName+" is not exist.");
            var priorities = _priorityDAL.GetPriorities();
            var statuses = _statusDAL.GetStatuses();
            var newTasks = _taskDAL.GetTasksByStatus(entity.StatusId).Select(x => new TaskInfo
            {
                TaskId = x.TaskId,
                TaskTitle = x.TaskTitle,
                TaskDescription = x.TaskDescription,
                TaskPriority = priorities.Find(priority => x.TaskPriority == priority.PriorityID).PriorityName,
                TaskCreatedDate = x.TaskCreatedDate,
                TaskDeadline = x.TaskDeadline,
                Assignee = _userDAL.GetById(x.Assignee).Username,
                TaskStatus = statuses.Find(status => x.TaskStatus == status.StatusId).StatusName,
            }).ToList();
            return newTasks;
        }

        public List<Models.TaskInfo> GetTasksByDeadline(string date)
        {
            DateTime myDate;
            if (!DateTime.TryParse(date, out myDate)) throw new InvalidDateException("the date is invalid");
            myDate = DateTime.Parse(date);
            var priorities = _priorityDAL.GetPriorities();
            var statuses = _statusDAL.GetStatuses();
            var newTasks = _taskDAL.GetTasksByDeadline(myDate).Select(x => new TaskInfo
            {
                TaskId = x.TaskId,
                TaskTitle = x.TaskTitle,
                TaskDescription = x.TaskDescription,
                TaskPriority = priorities.Find(priority => x.TaskPriority == priority.PriorityID).PriorityName,
                TaskCreatedDate = x.TaskCreatedDate,
                TaskDeadline = x.TaskDeadline,
                Assignee = _userDAL.GetById(x.Assignee).Username,
                TaskStatus = statuses.Find(status => x.TaskStatus == status.StatusId).StatusName,
            }).ToList();
            return newTasks;
        }

        public Models.Task GetTaskByID(int taskID)
        {
            return _taskDAL.GetTaskByID(taskID);
        }
    }
}
