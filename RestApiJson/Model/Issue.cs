using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ConsoleApp2.Model
{
    [DataContract(Name ="issue")]
    class Issue
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }

        [DataMember(Name = "project")]
        public Project Project { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "subject")]
        public string Subject { get; set; }

        public Issue(string id, Project project)
        {
            this.ID = id;
            this.Project = project;
        }

        public override string ToString()
        {
            return "Issue id: " + ID + ", subject: " + Subject + "," + Project;
        }
    }
}
