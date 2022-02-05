using Microsoft.AspNetCore.Mvc;
using Oversight_Project.Models;

namespace Oversight_Project.DAL
{
    public interface ITaskDAL
    {
        void Create(Models.Task task);
        void Delete(int taskID);
        void Edit(Models.Task task);
        List<Models.Task> WithinAWeek();
        List<Models.Task> GetTasksByStatus(int StatusID);
        List<Models.Task> GetTasksByPriority(int priorityID);
        List<Models.Task> GetAllTasks();
        List<Models.Task> GetTaskByUserID(int userID);
    }
}
