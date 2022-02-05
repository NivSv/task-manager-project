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
        public void Create(StatusClientInfo status)
        {
            _statusDAL.Create(status);
        }

        public void Delete(int statusID)
        {
            _statusDAL.Delete(statusID);
        }

        public List<Status> GetStatuses()
        {
            return _statusDAL.GetStatuses();
        }
    }
}
