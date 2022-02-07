using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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
        private readonly IUserAuthorizer _userAuthorizer;
        public TasksController(ITaskBL taskBL, IUserAuthorizer userAuthorizer)
        {
            _taskBL = taskBL;
            _userAuthorizer = userAuthorizer;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<Models.TaskInfo>> GetTasks([FromHeader, Required] string accessKey, [FromHeader, Required] string username)
        {
            if (!_userAuthorizer.isAuthorized(username, accessKey)) return StatusCode(401);
            return _taskBL.GetAllTasks();
        }

        [HttpGet]
        [Route("deadline/{date}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<TaskInfo>> GetTasksByDeadline([FromHeader, Required] string accessKey, [FromHeader, Required] string username, string date)
        {
            if (!_userAuthorizer.isAuthorized(username, accessKey)) return StatusCode(401);
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult DeleteTask([FromHeader, Required] string accessKey, [FromHeader, Required] string username, int taskID)
        {
            if (!_userAuthorizer.isAuthorized(username, accessKey)) return StatusCode(401);
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
        [Route("status/{status}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<TaskInfo>> GetTasksByStatus([FromHeader, Required] string accessKey, [FromHeader, Required] string username,string status)
        {
            if (!_userAuthorizer.isAuthorized(username, accessKey)) return StatusCode(401);
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
        [Route("{taskid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<Models.Task> GetTaskByID([FromHeader, Required] string accessKey, [FromHeader, Required] string username,int taskid)
        {
            if (!_userAuthorizer.isAuthorized(username, accessKey)) return StatusCode(401);
            try
            {
                return _taskBL.GetTaskByID(taskid);
            }
            catch (TaskNotExistsException e)
            {
                return StatusCode(404, e.Message);
            }
        }

        [HttpGet]
        [Route("priority/{priority}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<TaskInfo>> GetTasksByPriorty([FromHeader, Required] string accessKey, [FromHeader, Required] string username,string priority)
        {
            if (!_userAuthorizer.isAuthorized(username, accessKey)) return StatusCode(401);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult CreateTask([FromHeader, Required] string accessKey, [FromHeader, Required] string username,[FromBody] TaskCreationInfo task)
        {
            if (!_userAuthorizer.isAuthorized(username, accessKey)) return StatusCode(401);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult EditTask([FromHeader, Required] string accessKey, [FromHeader, Required] string username,[FromBody] TaskCreationInfo task,int taskID)
        {
            if (!_userAuthorizer.isAuthorized(username, accessKey)) return StatusCode(401);
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