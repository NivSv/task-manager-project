using Microsoft.AspNetCore.Mvc;
using TaskManagerBackend.BL;
using TaskManagerBackend.Models;


namespace TaskManagerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrioritiesController : ControllerBase
    {
        private readonly IPriorityBL _priorityBL;
        public PrioritiesController(IPriorityBL priorityBL)
        {
            _priorityBL = priorityBL;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult<List<Priority>> GetPriorities()
        {
            return _priorityBL.GetPriorities();
        }
    }
}