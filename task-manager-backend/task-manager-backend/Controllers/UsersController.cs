using Microsoft.AspNetCore.Mvc;
using TaskManagerBackend.BL;
using TaskManagerBackend.Exceptions;
using TaskManagerBackend.Models;


namespace TaskManagerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserBL _userBL;
        public UsersController(IUserBL userBL)
        {
            _userBL = userBL;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost]
        public ActionResult<string> Register([FromBody] RegisterInfo user)
        {
            try{
                _userBL.Register(user);
                return StatusCode(200);
            }
            catch (UserAlreadyExistsException e)
            {
                 return StatusCode(409, e.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult<List<UserInfo>> GetUsers()
        {
            return _userBL.GetAll();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("/user/{username}")]
        public ActionResult<UserInfo> GetUser(string username)
        {
            try
            {
                return _userBL.GetByUsername(username);
            }
            catch (UserNotExistsException e)
            {
                return StatusCode(404, e.Message);
            }
        }
    }
}