using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace FundaApp.Models.Client
{
    [DataContract]
    public class Paging
    {
        [DataMember]
        public int AantalPaginas { get; set; } // total page count

        [DataMember]
        public int HuidigePagina { get; set; } // current page
    }
}
