using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ConsoleApp2.Model
{
    [DataContract(Name = "projects")]
    class Projects
    {
        [DataMember(Name = "projects")]
        public Project[] projects { get; set; }
    }
}
