using Microsoft.AspNetCore.Mvc;
using TaskManagerBackend.BL;
using TaskManagerBackend.Exceptions;
using TaskManagerBackend.Models;


namespace TaskManagerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskBL _taskBL;
        public TaskController(ITaskBL taskBL)
        {
            _taskBL = taskBL;
        }

        [HttpGet]
        public ActionResult<List<TaskManagerBackend.Models.Task>> GetTasks()
        {
            return _taskBL.GetAllTasks();
        }

        [HttpGet]
        [Route("deadline/{date}")]
        public ActionResult<List<TaskManagerBackend.Models.Task>> GetTasksByDeadline(string date)
        {
            return _taskBL.GetTasksByDeadline(date);
        }

        [HttpDelete]
        [Route("{taskID}")]
        public ActionResult DeleteTask(int taskID)
        {
            try
            {
                _taskBL.Delete(taskID);
                return StatusCode(200);
            }
            catch (TaskNotExistsException e)
            {
                return StatusCode(404,e.Message);
            }
        }

        [HttpGet]
        [Route("/status/{status}")]
        public ActionResult<List<TaskManagerBackend.Models.Task>> GetTasksByStatus(string status)
        {
            try
            {
                return _taskBL.GetTasksByStatus(status);
            }
            catch (InvalidStatusException e)
            {
                return StatusCode(409, e.Message);
            }
        }

        [HttpGet]
        [Route("/priority/{priority}")]
        public ActionResult<List<TaskManagerBackend.Models.Task>> GetTasksByPriorty(string priority)
        {
            return _taskBL.GetTasksByPriority(priority);
        }

        [HttpPost]
        public ActionResult CreateTask([FromBody] TaskCreateInfo task)
        {
            try
            {
                _taskBL.Create(task);
                return StatusCode(200);
            }
            catch(UserNotExistsException e)
            {
                return StatusCode(404, e.Message);
            }
            catch(InvalidPriorityException e)
            {
                return StatusCode(404, e.Message);
            }
        }

        [HttpPut]
        public ActionResult EditTask([FromBody] TaskManagerBackend.Models.Task task)
        {
            _taskBL.Edit(task);
            return StatusCode(200);
        }
    }
}