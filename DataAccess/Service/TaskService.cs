using Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            TaskItem task = db.Tasks.Where(t => t.Id == id).Select(t => t).FirstOrDefault();
            if (task == null)
            {
                throw new ServiceException("Task not found");
            }

            task.DependentTasks = GetDependentTasksById(id);
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
                UpadteTaskDependencies(task.DependentTasks, task.Id);

                var taskTosave = db.Tasks.Find(task.Id);

                //can use automapper
                taskTosave.DependentTasks = null;
                taskTosave.Description = task.Description;
                taskTosave.Title = task.Title;
                taskTosave.Complete = task.Complete;

                db.Entry(taskTosave).State = System.Data.Entity.EntityState.Modified;
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

        private void UpadteTaskDependencies(IEnumerable<DependentTask> updatedTasks, Guid taskItemId)
        {
            try
            {
                var dependentTasks = GetDependentTasksById(taskItemId);

                if (dependentTasks.Any())
                {
                    db.DependentTasks.RemoveRange(dependentTasks);
                }

                if (updatedTasks != null)
                {
                    db.DependentTasks.AddRange(updatedTasks);
                }

                if (updatedTasks != null || dependentTasks.Any())
                {
                    //only save if we need to
                    if (db.SaveChanges() <= 0)
                    {
                        throw new Exception("Add Task Dependencies failed to add any records");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException("Something went wrong add task dependencies", ex);
            }
        }

        public List<DependentTask> GetDependentTasksById(Guid id)
        {
            return db.DependentTasks.Where(d => d.TaskItemId == id).ToList();
        }
    }
}