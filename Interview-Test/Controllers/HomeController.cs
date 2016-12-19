using Entity;
using Interview_Test.Utility;
using Interview_Test.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Interview_Test.Controllers
{
    public class HomeController : Controller
    {
        private TaskApiClient taskClient;

        public HomeController()
        {
            // Should use DI
            taskClient = new TaskApiClient();
        }

        public async Task<ActionResult> Index()
        {
            IndexViewModel viewModel = new IndexViewModel();

            IEnumerable<TaskItem> tasks = await taskClient.GetAllTasks();

            if (tasks != null)
            {
                viewModel.TaskList = tasks;
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(IndexViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Uri taskLocation = await taskClient.CreateTask(viewModel.CreateTaskModel);

                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<ActionResult> Detail(string id)
        {
            Guid editedTaskId;

            if (!Guid.TryParse(id, out editedTaskId))
            {
                return RedirectToAction("Index");
            }

            TaskItem task = await taskClient.GetTaskById(id);

            ///Get all Task for selection
            var tasks = await taskClient.GetAllTasks();

            var viewModel = new DetailViewModel();
            // should use a view builder
            viewModel.TasksToselect = GetTaskToSelect(task.Id, tasks);
            viewModel.EditedTaskItem = task;
            viewModel.DependentTask = task.DependentTasks.Select(t => t.DependentTaskItemId);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Detail(DetailViewModel viewModel)
        {
            if (viewModel.DependentTask != null)
            {
                viewModel.EditedTaskItem.DependentTasks = viewModel.DependentTask.Select(t => new DependentTask
                {
                    DependentTaskItemId = t,
                    TaskItemId = viewModel.EditedTaskItem.Id
                }).ToList();
            }

            var updated = await taskClient.Update(viewModel.EditedTaskItem);

            if (!updated)
            {
                ModelState.AddModelError(string.Empty, "Unable to update task, make sure all dependent task are completed.");

                // would have been cache in Live application
                var tasks = await taskClient.GetAllTasks();
                viewModel.TasksToselect = GetTaskToSelect(viewModel.EditedTaskItem.Id, tasks);
                return View(viewModel);
            }

            return RedirectToAction("Index");
        }

        private IEnumerable<SelectListItem> GetTaskToSelect(Guid editedTaskId, IEnumerable<TaskItem> tasks)
        {
            return tasks?.Where(t => t.Id != editedTaskId).Select(t => new SelectListItem
            {
                Text = t.Title,
                Value = t.Id.ToString()
            });
        }
    }
}