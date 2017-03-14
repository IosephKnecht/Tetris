using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ConsoleApp2.Model
{
    [DataContract]
    class NewIssues
    {
        [DataMember(Name ="issue")]
        public NewIssue nw { get; set; }
    }
}
