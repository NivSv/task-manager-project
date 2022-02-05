using Microsoft.AspNetCore.Mvc;
using TaskManagerBackend.BL;
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
        [Route("GetTasks")]
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

        [HttpGet]
        [Route("DeleteTask")]
        public ActionResult DeleteTask([FromBody] int taskID)
        {
            _taskBL.Delete(taskID);
            return StatusCode(200);
        }

        [HttpGet]
        [Route("GetTasksByStatus")]
        public ActionResult<List<TaskManagerBackend.Models.Task>> GetTasksByStatus([FromBody] int statusID)
        {
            return _taskBL.GetTasksByStatus(statusID);
        }

        [HttpGet]
        [Route("GetTasksByPriorty")]
        public ActionResult<List<TaskManagerBackend.Models.Task>> GetTasksByPriorty([FromBody] int priorityID)
        {
            return _taskBL.GetTasksByPriority(priorityID);
        }

        [HttpPost]
        [Route("CreateTask")]
        public ActionResult CreateTask([FromBody] TaskManagerBackend.Models.Task task)
        {
            _taskBL.Create(task);
            return StatusCode(200);
        }

        [HttpPut]
        [Route("EditTask")]
        public ActionResult EditTask([FromBody] TaskManagerBackend.Models.Task task)
        {
            _taskBL.Edit(task);
            return StatusCode(200);
        }
    }
}