using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ConsoleApp2.Model
{
    [DataContract]
    class Issues
    {
        [DataMember(Name = "issues")]
        public Issue[] issues { get; set; }
    }
}
