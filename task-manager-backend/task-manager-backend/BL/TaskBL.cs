using TaskManagerBackend.Models;
using TaskManagerBackend.DAL;

namespace TaskManagerBackend.BL
{
    public class TaskBL : ITaskBL
    {
        private readonly ITaskDAL _taskDAL;
        public TaskBL(ITaskDAL taskDAL)
        {
            _taskDAL = taskDAL;
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

        public List<Models.Task> GetTasksByStatus(int StatusID)
        {
            return _taskDAL.GetTasksByStatus(StatusID);
        }

        public List<Models.Task> WithinAWeek()
        {
            return _taskDAL.WithinAWeek();
        }
    }
}
