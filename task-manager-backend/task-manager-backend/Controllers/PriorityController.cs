using Microsoft.AspNetCore.Mvc;
using TaskManagerBackend.BL;
using TaskManagerBackend.Models;


namespace TaskManagerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriorityController : ControllerBase
    {
        private readonly IPriorityBL _priorityBL;
        public PriorityController(IPriorityBL priorityBL)
        {
            _priorityBL = priorityBL;
        }

        [HttpGet]
        public ActionResult<List<Priority>> GetPriorities()
        {
            return _priorityBL.GetPriorities();
        }
    }
}