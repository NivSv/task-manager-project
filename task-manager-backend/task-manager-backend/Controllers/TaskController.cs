using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Oversight_Project;
using Oversight_Project.Models;
using Oversight_Project.Utilities;


namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {

        /* Post method to register a new user (using a hash function to store the passwords in the DB). https://en.wikipedia.org/wiki/Hash_function
         * Parameters: Username and Password.
         * Return: StatusCode: 200 - if the server succeeded to register the new user, 
         * 409 - if the username provided is already exist in the DB.
         * 400 - if the user didn't provide a proper information.
         */
        [HttpGet]
        public ActionResult<string> GetTasks()
        {
            using var context = new TaskManagerContext();
            var tasks2 = context.Users.First();
            var tasks = context.Tasks.ToList();
            return JsonConvert.SerializeObject(tasks);
        }
    }
}