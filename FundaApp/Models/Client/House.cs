using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace FundaApp.Models.Client
{
    [DataContract]
    public class House
    {
        [DataMember]
        public int MakelaarId { get; set; }

        [DataMember]
        public String MakelaarNaam { get; set; }
    }
}
