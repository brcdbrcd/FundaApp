using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundaApp.Models.Out
{
    public class TopMakelaars
    {
        public int MakelaarId { get; set; }

        public String MakelaarName { get; set; }

        public int ObjCount { get; set; }

        public TopMakelaars(int _MakelaarId, String _MakelaarName, int _ObjCount)
        {
            this.MakelaarId = _MakelaarId;
            this.MakelaarName = _MakelaarName;
            this.ObjCount = _ObjCount;
        }
    }
}
