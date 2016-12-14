using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Interview_Test.Utility
{
    public abstract class ApiClient
    {
        protected HttpClient client;

        public ApiClient()
        {
            string baseUrl = "http://localhost:3411/task/";

            // trying something that will break the project, remove this line below!
            //string baseUrl = "http://CHRIS-PC/";

            client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        
    }
}