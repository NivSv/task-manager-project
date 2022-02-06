using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TaskManagerBackend.BL;
using TaskManagerBackend.Models;


namespace TaskManagerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrioritiesController : ControllerBase
    {
        private readonly IPriorityBL _priorityBL;
        private readonly IUserAuthorizer _userAuthorizer;
        public PrioritiesController(IPriorityBL priorityBL, IUserAuthorizer userAuthorizer)
        {
            _priorityBL = priorityBL;
            _userAuthorizer = userAuthorizer;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<Priority>> GetPriorities([FromHeader, Required] string accessKey, [FromHeader, Required] string username)
        {
            if (!_userAuthorizer.isAuthorized(username, accessKey)) return StatusCode(401);
            return _priorityBL.GetPriorities();
        }
    }
}