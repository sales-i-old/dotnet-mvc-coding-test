using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Entity
{
    [Serializable]
    public class TaskItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id
        { get; set; }

        [Required(ErrorMessage = "You must enter a task title")]
        public string Title
        { get; set; }

        [Required]
        public string Description
        { get; set; }

        public bool Complete
        { get; set; }

        public virtual ICollection<DependentTask> DependentTasks { get; set; }
    }
}