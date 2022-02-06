using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TaskManagerBackend.BL;
using TaskManagerBackend.Models;


namespace TaskManagerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        private readonly IUserAuthorizer _userAuthorizer;
        private readonly IStatusBL _statusBL;
        public StatusesController(IStatusBL statusBL, IUserAuthorizer userAuthorizer)
        {
            _statusBL = statusBL;
            _userAuthorizer = userAuthorizer;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<Status>> GetStatuses([FromHeader, Required] string accessKey, [FromHeader, Required] string username)
        {
            if (!_userAuthorizer.isAuthorized(username, accessKey)) return StatusCode(401);
            return _statusBL.GetStatuses();
        }
    }
}