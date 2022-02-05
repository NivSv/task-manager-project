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
        public ActionResult<List<Models.Task>> GetTasks()
        {
            return _taskBL.GetAllTasks();
        }

        [HttpGet]
        [Route("deadline/{date}")]
        public ActionResult<List<Models.Task>> GetTasksByDeadline(string date)
        {
            try
            {
                return _taskBL.GetTasksByDeadline(date);
            }
            catch(InvalidDateException e)
            {
                return StatusCode(422, e.Message);
            }
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
        public ActionResult<List<Models.Task>> GetTasksByStatus(string status)
        {
            try
            {
                return _taskBL.GetTasksByStatus(status);
            }
            catch (InvalidStatusException e)
            {
                return StatusCode(422, e.Message);
            }
        }

        [HttpGet]
        [Route("/priority/{priority}")]
        public ActionResult<List<Models.Task>> GetTasksByPriorty(string priority)
        {
            try
            {
                return _taskBL.GetTasksByPriority(priority);
            }
            catch(InvalidPriorityException e)
            {
                return StatusCode(422, e.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateTask([FromBody] TaskInfo task)
        {
            try
            {
                _taskBL.Create(task);
                return StatusCode(200);
            }
            catch(UserNotExistsException e)
            {
                return StatusCode(422, e.Message);
            }
            catch(InvalidPriorityException e)
            {
                return StatusCode(422, e.Message);
            }
            catch(InvalidStatusException e)
            {
                return StatusCode(422, e.Message);
            }
        }

        [HttpPut]
        [Route("{taskid}")]
        public ActionResult EditTask([FromBody] TaskInfo task,int taskID)
        {
            try
            {
                _taskBL.Edit(task, taskID);
                return StatusCode(200);
            }
            catch (UserNotExistsException e)
            {
                return StatusCode(422, e.Message);
            }
            catch (InvalidPriorityException e)
            {
                return StatusCode(422, e.Message);
            }
            catch (InvalidStatusException e)
            {
                return StatusCode(422, e.Message);
            }
            catch(TaskNotExistsException e)
            {
                return StatusCode(404, e.Message);
            }
        }
    }
}