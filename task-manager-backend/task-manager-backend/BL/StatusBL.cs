using TaskManagerBackend.DAL;
using TaskManagerBackend.Models;

namespace TaskManagerBackend.BL
{
    public class StatusBL : IStatusBL
    {
        private readonly IStatusDAL _statusDAL;
        public StatusBL(IStatusDAL statusDAL)
        {
            _statusDAL = statusDAL;
        }
        public List<Status> GetStatuses()
        {
            return _statusDAL.GetStatuses();
        }
    }
}
