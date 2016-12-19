using DataAccess.Factory;
using DataAccess.Service;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Interview_Test.Utility
{
    public class TaskApiClient : ApiClient
    {
       
        public async Task<IEnumerable<TaskItem>> GetAllTasks()
        {
            IEnumerable<TaskItem> tasks = null;

            HttpResponseMessage response = await client.GetAsync(client.BaseAddress);

            if (response.IsSuccessStatusCode)
            {
                tasks = await response.Content.ReadAsAsync<IEnumerable<TaskItem>>();
            }

            return tasks;
        }

        public async Task<TaskItem> GetTaskById(string id)
        {
            TaskItem task = null;

            HttpResponseMessage response = await client.GetAsync(id);

            if (response.IsSuccessStatusCode)
            {
                task = await response.Content.ReadAsAsync<TaskItem>();
            }

            return task;
        }

        public async Task<Uri> CreateTask(TaskItem task)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("", task);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                return response.Headers.Location;
            }
        }

        public async Task<bool> Update(TaskItem task)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync("", task);

            return response.IsSuccessStatusCode;
        }
    }
}