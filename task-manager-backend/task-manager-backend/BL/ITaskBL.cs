using TaskManagerBackend.Models;

namespace TaskManagerBackend.BL
{
    public interface ITaskBL
    {
        void Create(TaskCreationInfo task);
        void Delete(int taskID);
        void Edit(TaskCreationInfo task, int taskID);
        List<TaskInfo> GetTasksByDeadline(string date);
        List<TaskInfo> GetTasksByStatus(string statusName);
        List<TaskInfo> GetTasksByPriority(string priorityName);
        List<TaskInfo> GetAllTasks();
        List<TaskInfo> GetTaskByUserID(int userID);
        Models.Task GetTaskByID(int taskID);
    }
}