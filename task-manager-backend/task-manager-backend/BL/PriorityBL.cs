using TaskManagerBackend.DAL;
using TaskManagerBackend.Models;

namespace TaskManagerBackend.BL
{
    public class PriorityBL : IPriorityBL
    {
        private readonly IPriorityDAL _priorityDAL;
        public PriorityBL(IPriorityDAL priorityDAL)
        {
            _priorityDAL = priorityDAL;
        }
        public List<Priority> GetPriorities()
        {
            return _priorityDAL.GetPriorities();
        }
    }
}
