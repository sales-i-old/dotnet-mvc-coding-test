using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class TaskService : ITaskService
    {
        private TaskDbContext db;

        public TaskService(TaskDbContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// Get all the tasks in the database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TaskItem> GetAllTasks()
        {
            return db.Tasks.AsEnumerable();
        }

        /// <summary>
        /// Get a task by its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TaskItem GetTaskById(Guid id)
        {
            TaskItem task = (from t in db.Tasks where t.Id == id select t).FirstOrDefault();

            if (task == null)
            {
                throw new ServiceException("Task not found");
            }

            return task;
        }

        /// <summary>
        /// Add a new task
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public Guid CreateTask(TaskItem task)
        {
            try
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return task.Id;
            }
            catch (Exception ex)
            {
                throw new ServiceException("Something went wrong creating a new Task", ex);
            }
        }

        public void UpdateTask(TaskItem task)
        {
            try
            {
                db.Entry(task).State = System.Data.Entity.EntityState.Modified;

                if (db.SaveChanges() <= 0)
                {
                    throw new Exception("Update statement failed to change any records");
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException("Task not updated", ex);
            }
        }
    }
}
