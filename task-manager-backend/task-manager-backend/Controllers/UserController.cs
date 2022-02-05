using Microsoft.AspNetCore.Mvc;
using TaskManagerBackend.BL;
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
        [Route("Register")]
        public ActionResult<string> Register([FromBody] RegisterInfo user)
        {
            _userBL.Register(user);
            return StatusCode(200);
        }

        [HttpGet]
        [Route("GetUsers")]
        public ActionResult<List<User>> GetUsers()
        {
            return _userBL.GetAll();
        }

        [HttpGet]
        [Route("GetUser")]
        public ActionResult<User> GetUser([FromQuery] string username)
        {
            return _userBL.GetByUsername(username);
        }
    }
}