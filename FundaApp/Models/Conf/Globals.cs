using FundaApp.Models.Out;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundaApp.Models.Conf
{
    public static class Globals
    {
        // for providing the data import status and last updated time in the output
        public static String ImportStatus { get; set; }
        public static DateTime LastUpdated { get; set; }
    }
}
