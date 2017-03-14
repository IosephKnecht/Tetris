using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ConsoleApp2.Model
{
    [DataContract(Name ="issue")]
    class NewIssue
    {
        [DataMember(Name = "project_id")]
        public string Project_Id { get; set; }

        [DataMember(Name = "subject")]
        public string Subject { get; set; }
    }
}
