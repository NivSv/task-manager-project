using Microsoft.AspNetCore.Mvc;
using TaskManagerBackend.BL;
using TaskManagerBackend.Exceptions;
using TaskManagerBackend.Models;


namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL _userBL;
        public UserController(IUserBL userBL)
        {
            _userBL = userBL;
        }

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

        [HttpGet]
        public ActionResult<List<UserInfo>> GetUsers()
        {
            return _userBL.GetAll();
        }

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