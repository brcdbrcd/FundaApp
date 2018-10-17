using FundaApp.Models.Out;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundaApp.Service
{
    public interface IFundaService
    {
        Task ImportData();
        
        Task<Output> GetTopMakelaars(bool tuin);
    }
}
