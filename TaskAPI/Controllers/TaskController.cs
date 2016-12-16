using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Entity;
using DataAccess.Service;
using DataAccess.Factory;
using System.Web.Http.Description;
using System;

namespace TaskAPI.Controllers
{
    public class TaskController : ApiController
    {
        private ITaskService taskService;

        public TaskController()
        {
            // Should use DI
            TaskServiceFactory taskServiceFactory = new TaskServiceFactory();
            taskService = taskServiceFactory.Create();
        }

        [ResponseType(typeof(TaskItem))]
        public IHttpActionResult GetTasks()
        {
            // get all tasks
            IEnumerable<TaskItem> tasks = taskService.GetAllTasks();
            
            return Ok(tasks);
        }

        [ResponseType(typeof(TaskItem))]
        public IHttpActionResult GetTaskById(string id)
        {
            try
            {
                // get a task using its id
                TaskItem task = taskService.GetTaskById(new Guid(id));
                return Ok(task);
            }
            catch (Exception ex)
            {
                return NotFound();
            }            
        }

        [HttpPost]
        public IHttpActionResult AddTask(TaskItem task)
        {
            if (task == null)
            {
                return BadRequest("Task not received");
            }

            Guid id = taskService.CreateTask(task);

            return CreatedAtRoute("GetTaskById", id.ToString(), task);
        }

        [HttpPut]
        public IHttpActionResult UpdateTask(TaskItem task)
        {
            
            if (task == null)
            {
                return BadRequest("Task not received");
            }

              taskService.UpdateTask(task);

            return CreatedAtRoute("GetTaskById", task.Id.ToString(), task);
        }

    }
}
