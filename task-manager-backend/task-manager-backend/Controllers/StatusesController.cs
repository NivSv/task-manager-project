using Microsoft.AspNetCore.Mvc;
using TaskManagerBackend.BL;
using TaskManagerBackend.Models;


namespace TaskManagerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        private readonly IStatusBL _statusBL;
        public StatusesController(IStatusBL statusBL)
        {
            _statusBL = statusBL;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult<List<Status>> GetStatuses()
        {
            return _statusBL.GetStatuses();
        }
    }
}