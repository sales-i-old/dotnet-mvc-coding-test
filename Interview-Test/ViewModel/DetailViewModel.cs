using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Interview_Test.ViewModel
{
    public class DetailViewModel
    {
        public TaskItem EditedTaskItem { get; set; }

        [Display(Name = "Dependent Task")]
        public IEnumerable<Guid> DependentTask { get; set; }

        public IEnumerable<SelectListItem> TasksToselect { get; set; }
    }
}