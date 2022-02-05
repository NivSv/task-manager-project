﻿using TaskManagerBackend.Models;

namespace TaskManagerBackend.DAL
{
    public interface IStatusDAL
    {
        void Create(StatusClientInfo status);
        void Delete(int statusID);
        List<Status> GetStatuses();
    }
}
