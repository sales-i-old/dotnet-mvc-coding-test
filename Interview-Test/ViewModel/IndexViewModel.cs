using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Interview_Test.ViewModel
{
    public class IndexViewModel
    {
        public TaskItem CreateTaskModel
        { get; set; }

        public IEnumerable<TaskItem> TaskList
        { get; set; }

        public IndexViewModel()
        {
            // Instantiate TaskItem ready to be populated if required
            CreateTaskModel = new TaskItem();

            // Instantiate TaskList ready for tasks (if there are any)
            TaskList = new List<TaskItem>();
        }
    }
}