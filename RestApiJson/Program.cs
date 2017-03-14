using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            //GetProjectList get_project = new GetProjectList();
            //get_project.Get();

            //GetIssueList get_issue = new GetIssueList();
            //get_issue.Get();

            AddIssue addissue = new AddIssue();
            addissue.Post();
        }
    }
}
