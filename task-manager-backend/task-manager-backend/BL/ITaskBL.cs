using TaskManagerBackend.Models;

namespace TaskManagerBackend.BL
{
    public interface ITaskBL
    {
        void Create(TaskCreationInfo task);
        void Delete(int taskID);
        void Edit(TaskCreationInfo task, int taskID);
        List<Models.Task> GetTasksByDeadline(string date);
        List<Models.Task> GetTasksByStatus(string statusName);
        List<Models.Task> GetTasksByPriority(string priorityName);
        List<TaskInfo> GetAllTasks();
        List<Models.Task> GetTaskByUserID(int userID);
        Models.Task GetTaskByID(int taskID);
    }
}