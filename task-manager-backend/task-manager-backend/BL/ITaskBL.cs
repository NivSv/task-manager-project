﻿namespace TaskManagerBackend.BL
{
    public interface ITaskBL
    {
        void Create(Models.Task task);
        void Delete(int taskID);
        void Edit(Models.Task task);
        List<Models.Task> WithinAWeek();
        List<Models.Task> GetTasksByStatus(string statusName);
        List<Models.Task> GetTasksByPriority(string priorityName);
        List<Models.Task> GetAllTasks();
        List<Models.Task> GetTaskByUserID(int userID);
    }
}