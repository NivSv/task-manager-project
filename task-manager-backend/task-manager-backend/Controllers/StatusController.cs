using Microsoft.AspNetCore.Mvc;
using TaskManagerBackend.BL;
using TaskManagerBackend.Models;


namespace TaskManagerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusBL _statusBL;
        public StatusController(IStatusBL statusBL)
        {
            _statusBL = statusBL;
        }

        [HttpPost]
        [Route("CreateStatus")]
        public ActionResult<string> CreateStatus([FromBody] StatusClientInfo status)
        {
            _statusBL.Create(status);
            return StatusCode(200);
        }

        [HttpDelete]
        [Route("DeleteStatus")]
        public ActionResult<string> DeleteStatus([FromBody] int statusid)
        {
            _statusBL.Delete(statusid);
            return StatusCode(200);
        }
        
        [HttpGet]
        [Route("GetStatuses")]
        public ActionResult<List<Status>> GetStatuses()
        {
            return _statusBL.GetStatuses();
        }
    }
}