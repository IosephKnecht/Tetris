using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using System.Runtime.Serialization.Json;

namespace ConsoleApp2
{
    class GetProjectList
    {
        HttpClient IdHTTP = new HttpClient();

        public void Get()
        {
            IdHTTP.DefaultRequestHeaders.Add("X-Redmine-API-Key", "fb28e08f98c91a62efb6f749e783de3185a1f3e8");

            UriBuilder builder = new UriBuilder("https", "exactprotest.plan.io", -1, "projects.json");
            NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);
            //query["project_id"] = "4";
            builder.Query = query.ToString();

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, builder.Uri);
            message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Console.WriteLine(message.ToString());

            Task<HttpResponseMessage> taskResponse = IdHTTP.SendAsync(message);

            taskResponse.Wait();

            HttpResponseMessage response = taskResponse.Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(" response successful");

                Task<Stream> streamTask = response.Content.ReadAsStreamAsync();

                streamTask.Wait();

                if (streamTask.IsCompleted)
                {
                    Stream responseStream = streamTask.Result;

                    String result = null;

                    using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        result = reader.ReadToEnd();
                    }

                    using (StreamWriter file = new StreamWriter("ProjectList.txt"))
                    {
                        file.WriteLine(result);
                    }
                    
                    
                    IDictionary<string, Model.Project> issues = parseIssueJson(responseStream);

                    foreach (KeyValuePair<string, Model.Project> issuePair in issues)
                    {
                        Console.WriteLine(issuePair.Key + "<-->" + issuePair.Value);
                    }

                    responseStream.Close();
                    
                }
            }
            else
            {
                Console.WriteLine(" response failed. Response status code: [" + response.StatusCode + "]");
            }

            Console.ReadKey();
        }

        private  IDictionary<string, Model.Project> parseIssueJson(Stream dataStream)
        {
            IDictionary<string, Model.Project> projects = new Dictionary<string, Model.Project>();

            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Model.Projects));

            object obj = jsonSerializer.ReadObject(dataStream);

            Model.Projects data = obj as Model.Projects;

            foreach (Model.Project project in data.projects)
            {
                projects.Add(project.ID, project);
            }

            return projects;
        }
    }
}
