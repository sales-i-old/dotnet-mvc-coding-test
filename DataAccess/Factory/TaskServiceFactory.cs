using DataAccess.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Factory
{
    public class TaskServiceFactory
    {
        public TaskService Create()
        {
            return new TaskService(new TaskDbContext());
        }
    }
}
