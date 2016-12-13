using System;
using System.Collections.Generic;
using Entity;

namespace DataAccess.Service
{
    public interface ITaskService
    {
        Guid CreateTask(TaskItem task);
        IEnumerable<TaskItem> GetAllTasks();
        TaskItem GetTaskById(Guid id);
        void UpdateTask(TaskItem task);
    }
}