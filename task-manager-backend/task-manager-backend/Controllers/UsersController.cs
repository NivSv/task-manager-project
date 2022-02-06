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

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
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

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<string> Login([FromBody] RegisterInfo user)
        {
            try
            {
                return _userBL.Login(user);
            }
            catch (UserNotExistsException e)
            {
                return StatusCode(404, e.Message);
            }
            catch (InvalidPasswordException e)
            {
                return StatusCode(401, e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<UserInfo>> GetUsers()
        {
            return _userBL.GetAll();
        }

        [HttpGet]
        [Route("{username}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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