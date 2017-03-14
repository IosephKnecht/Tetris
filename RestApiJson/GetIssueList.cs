using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Specialized;
using System.Web;
using System.IO;
using System.Runtime.Serialization.Json;

namespace ConsoleApp2
{
    class GetIssueList
    {
        HttpClient IdHTTP = new HttpClient();

        public void Get()
        {

            //Adding Redmine API key for user Authentication . It is mine, please use yours
            IdHTTP.DefaultRequestHeaders.Add("X-Redmine-API-Key", "fb28e08f98c91a62efb6f749e783de3185a1f3e8");

            UriBuilder builder = new UriBuilder("https", "exactprotest.plan.io", -1, "issues.json");
            NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);
            query["project_id"] = "4";
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


                    //Save to file
                    /*
                    String result = null;

                    using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        result = reader.ReadToEnd();
                    }

                    using (StreamWriter file = new StreamWriter("IssueList.txt"))
                    {
                        file.WriteLine(result);
                    }
                    */


                    IDictionary<string, Model.Issue> issues = parseIssueJson(responseStream);

                    foreach (KeyValuePair<string, Model.Issue> issuePair in issues)
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

        private  IDictionary<string, Model.Issue> parseIssueJson(Stream dataStream)
        {
            IDictionary<string, Model.Issue> projects = new Dictionary<string, Model.Issue>();

            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Model.Issues));

            object obj = jsonSerializer.ReadObject(dataStream);

            Model.Issues data = obj as Model.Issues;

            foreach (Model.Issue issue in data.issues)
            {
                projects.Add(issue.ID, issue);
            }

            return projects;
        }
    }
}
