using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace FundaApp.Models.Client
{
    [DataContract]
    public class Response
    {
        [DataMember]
        public List<House> Objects { get; set; }

        [DataMember]
        public Paging Paging { get; set; }

        [DataMember]
        public int TotaalAantalObjecten { get; set; } // total object count
    }
}
