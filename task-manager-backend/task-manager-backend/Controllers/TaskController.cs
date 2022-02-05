using Microsoft.AspNetCore.Mvc;
using TaskManagerBackend.BL;
using TaskManagerBackend.Exceptions;
using TaskManagerBackend.Models;


namespace WebApplication1.Controllers
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
        [Route("GetTasksWithinAWeek")]
        public ActionResult<List<TaskManagerBackend.Models.Task>> GetTasksWithinAWeek()
        {
            return _taskBL.WithinAWeek();
        }

        [HttpDelete]
        public ActionResult DeleteTask([FromBody] int taskID)
        {
            _taskBL.Delete(taskID);
            return StatusCode(200);
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
        [Route("GetTasksByPriorty")]
        public ActionResult<List<TaskManagerBackend.Models.Task>> GetTasksByPriorty([FromBody] int priorityID)
        {
            return _taskBL.GetTasksByPriority(priorityID);
        }

        [HttpPost]
        public ActionResult CreateTask([FromBody] TaskManagerBackend.Models.Task task)
        {
            _taskBL.Create(task);
            return StatusCode(200);
        }

        [HttpPut]
        public ActionResult EditTask([FromBody] TaskManagerBackend.Models.Task task)
        {
            _taskBL.Edit(task);
            return StatusCode(200);
        }
    }
}