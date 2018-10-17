using FundaApp.Models.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FundaApp.Models.Out
{
    public class Output
    {
        public String ImportStatus { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool HasGarden { get; set; }

        public List<TopMakelaars> Top10Makelaars { get; set; }

        public Output(String _ImportStatus, DateTime _LastUpdated, bool _HasGarden, List<Makelaar> list)
        {
            this.ImportStatus = _ImportStatus;
            this.LastUpdated = _LastUpdated;
            this.HasGarden = _HasGarden;
            this.Top10Makelaars = new List<TopMakelaars>();
            foreach (Makelaar m in list)
            {
                Top10Makelaars.Add(new TopMakelaars(m.MakelaarId, m.MakelaarName, m.HouseCount));
            }
        }
    }
}
