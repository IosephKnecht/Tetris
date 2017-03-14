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
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace ConsoleApp2
{
    class AddIssue
    {
        HttpClient IdHTTP = new HttpClient();

        public void Post()
        {
            IdHTTP.DefaultRequestHeaders.Add("X-Redmine-API-Key", "fb28e08f98c91a62efb6f749e783de3185a1f3e8");

            UriBuilder builder = new UriBuilder("https", "exactprotest.plan.io", -1, "issues.json");
            NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);
            //query["project_id"] = "4";
            builder.Query = query.ToString();

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, builder.Uri);
            message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Console.WriteLine(message.ToString());

            var content = new FormUrlEncodedContent(new[]
                        {
                new KeyValuePair<string, string>("subject", "login")
            });

            var request = new
            {
                issue=new
                {
                    project_id = "4",
                    subject = "Example",
                    //priority_id = "1",
                }
            };
            var a = JsonConvert.SerializeObject(request);

            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(Model.NewIssues));
            MemoryStream stream1 = new MemoryStream();
            Model.NewIssues newissue = new Model.NewIssues
            {
                nw=new Model.NewIssue
                {
                    Project_Id = "4",
                    Subject = "Example",
                }
            };
            json.WriteObject(stream1, newissue);

            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            object obj = sr.ReadToEnd();
            Console.Write("JSON form of Person object: ");
            Console.WriteLine(sr.ReadToEnd());

            Task<HttpResponseMessage> taskResponse = IdHTTP.PostAsync(message.RequestUri,
                new StringContent(obj.ToString(),
                            Encoding.UTF8, "application/json"));


            taskResponse.Wait();
        }
    }
}
