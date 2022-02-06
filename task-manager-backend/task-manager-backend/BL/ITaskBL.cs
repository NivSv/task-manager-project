using TaskManagerBackend.Models;

namespace TaskManagerBackend.BL
{
    public interface ITaskBL
    {
        void Create(TaskInfo task);
        void Delete(int taskID);
        void Edit(TaskInfo task, int taskID);
        List<Models.Task> GetTasksByDeadline(string date);
        List<Models.Task> GetTasksByStatus(string statusName);
        List<Models.Task> GetTasksByPriority(string priorityName);
        List<Models.Task> GetAllTasks();
        List<Models.Task> GetTaskByUserID(int userID);
        Models.Task GetTaskByID(int taskID);
    }
}