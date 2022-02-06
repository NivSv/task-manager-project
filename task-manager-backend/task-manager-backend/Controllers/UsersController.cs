using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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
        private readonly IUserAuthorizer _userAuthorizer;
        public UsersController(IUserBL userBL,IUserAuthorizer userAuthorizer)
        {
            _userBL = userBL;
            _userAuthorizer = userAuthorizer;
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<UserInfo>> GetUsers([FromHeader, Required] string accessKey, [FromHeader, Required] string username)
        {
            if (!_userAuthorizer.isAuthorized(username, accessKey)) return StatusCode(401);
            return _userBL.GetAll();
        }

        [HttpGet]
        [Route("{username}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<UserInfo> GetUser([FromHeader, Required] string accessKey,string username)
        {
            if (!_userAuthorizer.isAuthorized(username, accessKey)) return StatusCode(401);
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