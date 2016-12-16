using Entity;
using Interview_Test.Utility;
using Interview_Test.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

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
            TaskItem task = await taskClient.GetTaskById(id);
            return View(task);
        }

        [HttpPost]
        public async Task<ActionResult> Detail(TaskItem task)
        {
            bool updated = await taskClient.Update(task);
            return RedirectToAction("Detail", new { id = task.Id});
        }

    }
}