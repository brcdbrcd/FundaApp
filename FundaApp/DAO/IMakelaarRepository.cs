using FundaApp.Models.DB;
using FundaApp.Models.Out;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundaApp.DAO
{
    public interface IMakelaarRepository
    {
        Task InsertOrUpdateMakelaar(Makelaar item);

        Task DeleteAll();

        Task<Output> GetTopMakelaars(bool tuin);
    }
}
