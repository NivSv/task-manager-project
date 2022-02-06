using Microsoft.AspNetCore.Mvc;
using TaskManagerBackend.BL;
using TaskManagerBackend.Exceptions;
using TaskManagerBackend.Models;


namespace TaskManagerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskBL _taskBL;
        public TasksController(ITaskBL taskBL)
        {
            _taskBL = taskBL;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult<List<Models.Task>> GetTasks()
        {
            return _taskBL.GetAllTasks();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
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

        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        ///<returns></returns>
        ///<response code="200">If the task got deleted</response>
        ///<response code="404">If the task is not exist.</response>
        [HttpDelete]
        [Route("{taskID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpGet]
        [Route("status/{status}")]
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("{taskid}")]
        public ActionResult<Models.Task> GetTaskByID(int taskid)
        {
            try
            {
                return _taskBL.GetTaskByID(taskid);
            }
            catch (TaskNotExistsException e)
            {
                return StatusCode(404, e.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpGet]
        [Route("priority/{priority}")]
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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