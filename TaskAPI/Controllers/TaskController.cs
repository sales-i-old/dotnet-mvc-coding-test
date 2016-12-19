using DataAccess.Factory;
using DataAccess.Service;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

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

            return Ok(tasks.ToList());
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

            if (task.Complete)
            {
                if (!CanCompleteTask(task))
                {
                    return BadRequest("Cannot complete this task some dependent task are not completed");
                }
            }

            taskService.UpdateTask(task);

            return Ok();

        }

        private bool CanCompleteTask(TaskItem task)
        {
            var canComplete = true;

            IEnumerable<TaskItem> allTasks = taskService.GetAllTasks();

            if (task.DependentTasks?.Count() > 0 && allTasks != null)
            {
                canComplete = !allTasks.Where(t => task.DependentTasks.Select(d => d.DependentTaskItemId).Contains(t.Id) && !t.Complete).Any();
            }

            return canComplete;
        }
    }
}