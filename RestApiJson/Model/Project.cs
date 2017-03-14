using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ConsoleApp2.Model
{
    [DataContract(Name = "project")]
    class Project
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }

        [DataMember(Name ="name")]
        public string Name { get; set; }

        [DataMember(Name = "identifier")]
        public string Identifier { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        public override string ToString()
        {
            return  Name + " " + Identifier + " " + Status;
        }
    }
}
