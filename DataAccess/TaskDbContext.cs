using Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class TaskDbContext : DbContext
    {
        public DbSet<TaskItem> Tasks
        { get; set; }

        public TaskDbContext()
            : base("name=TaskDbContext")
        {
            
        }
    }
}
