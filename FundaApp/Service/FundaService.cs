using FundaApp.DAO;
using FundaApp.Logger;
using FundaApp.Models.Client;
using FundaApp.Models.Conf;
using FundaApp.Models.DB;
using FundaApp.Models.Out;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FundaApp.Service
{
    public class FundaService : IFundaService
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly IMakelaarRepository makelaarRepository;
        private readonly String uri1;
        private readonly String uri2;
        private ILoggerManager logger;

        public FundaService(IMakelaarRepository _makelaarRepository, IOptions<Settings> _settings, ILoggerManager _logger)
        {
            makelaarRepository = _makelaarRepository;
            uri1 = _settings.Value.FundaUri1;
            uri2 = _settings.Value.FundaUri2;
            logger = _logger;
        }

        // Imports data from Funda REST Api to local - MongoDB
        public async Task ImportData()
        {
            try
            {
                await makelaarRepository.DeleteAll();
                logger.LogInfo("Importing Data.");

                Globals.ImportStatus = "Data importing is in progress...";
                
                await Import(uri1, false);
                await Import(uri2, true);

                Globals.ImportStatus = "Data importing is finished";
                Globals.LastUpdated = DateTime.Now;
            }
            catch (Exception ex)
            {
                logger.LogError("Something went wrong while importing data. Details: " + ex.StackTrace);
            }
        }

        private async Task Import(string requestUri, bool hasGarden)
        {
            client.DefaultRequestHeaders.Accept.Clear();

            int CurrentPage = 1;
            int TotalPage = int.MaxValue;
            while (CurrentPage < TotalPage)
            {
                var stringTask = client.GetStringAsync(requestUri + CurrentPage.ToString());
                var msg = await stringTask;

                Response resp = JsonConvert.DeserializeObject<Response>(msg);

                if (resp != null && resp.Objects != null)
                {
                    foreach (House house in resp.Objects)
                    {
                        Makelaar makelaar = new Makelaar
                        {
                            MakelaarId = house.MakelaarId,
                            MakelaarName = house.MakelaarNaam,
                            HouseCount = hasGarden ? 0 : 1,
                            HouseWithGardenCount = hasGarden ? 1 : 0
                        };

                        await makelaarRepository.InsertOrUpdateMakelaar(makelaar);
                    }
                }

                TotalPage = resp.Paging.AantalPaginas;
                CurrentPage = resp.Paging.HuidigePagina + 1;
                Task.Delay(600).Wait(); // approximately 100 requests per minute are allowed
            }
        }

        public async Task<Output> GetTopMakelaars(bool tuin)
        {
            return await makelaarRepository.GetTopMakelaars(tuin);
        }
    }
}
