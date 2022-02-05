using TaskManagerBackend.Models;
using TaskManagerBackend.DAL;
using TaskManagerBackend.Exceptions;

namespace TaskManagerBackend.BL
{
    public class TaskBL : ITaskBL
    {
        private readonly ITaskDAL _taskDAL;
        private readonly IStatusDAL _statusDAL;
        public TaskBL(ITaskDAL taskDAL, IStatusDAL statusDAL)
        {
            _taskDAL = taskDAL;
            _statusDAL = statusDAL;
        }
        public void Create(Models.Task task)
        {
            _taskDAL.Create(task);
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

        public List<Models.Task> GetTasksByPriority(int priorityID)
        {
            return _taskDAL.GetTasksByPriority(priorityID);
        }

        public List<Models.Task> GetTasksByStatus(string statusName)
        {
            var entity = _statusDAL.GetStatuses().Find(status => status.StatusName.ToLower() == statusName.ToLower());
            if (entity == null) throw new InvalidStatusException("Status "+statusName+" is not exist.");
            return _taskDAL.GetTasksByStatus(entity.StatusId);
        }

        public List<Models.Task> WithinAWeek()
        {
            return _taskDAL.WithinAWeek();
        }
    }
}
