using FundaApp.Logger;
using FundaApp.Models.Conf;
using FundaApp.Models.DB;
using FundaApp.Models.Out;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FundaApp.DAO
{
    public class MakelaarRepository : IMakelaarRepository
    {
        private readonly MakelaarContext context = null;
        private ILoggerManager logger;
        private IOptions<Settings> settings;

        public MakelaarRepository(IOptions<Settings> _settings, ILoggerManager _logger)
        {
            context = new MakelaarContext(_settings);
            logger = _logger;
            settings = _settings;
        }

        public async Task InsertOrUpdateMakelaar(Makelaar item)
        {
            Makelaar temp = GetMakelaar(item);
            if (temp == null)
            {
                await AddMakelaar(item);
                return;
            }

            item.HouseCount += temp.HouseCount;
            item.HouseWithGardenCount += temp.HouseWithGardenCount;
            item.InternalId = temp.InternalId;

            await UpdateMakelaar(item);
        }

        public async Task DeleteAll()
        {
            await context.Makelaars.DeleteManyAsync(x => true);
        }

        private Makelaar GetMakelaar(Makelaar item)
        {
            try
            {
                List<Makelaar> l = context.Makelaars.Find(x => x.MakelaarId == item.MakelaarId).ToListAsync().Result;
                if (l.Count > 0)
                {
                    return l.First();
                }
            }
            catch (Exception ex)
            {
                logger.LogWarn("Makelaar Repository seems to be empty.");
            }
            return null;
        }

        private async Task AddMakelaar(Makelaar item)
        {
            try
            {
                await context.Makelaars.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        private async Task UpdateMakelaar(Makelaar item)
        {
            try
            {
                // ReplaceOneAsync
                await context.Makelaars.FindOneAndReplaceAsync(x => x.MakelaarId == item.MakelaarId, item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<Output> GetTopMakelaars(bool tuin)
        {
            List<Makelaar> list;

            if (!tuin)
            {
                list = await context.Makelaars.Find(x => true).SortByDescending(d => d.HouseCount).Limit(10).ToListAsync();
            }
            else
            {
                list = await context.Makelaars.Find(x => true).SortByDescending(d => d.HouseWithGardenCount).Limit(10).ToListAsync();
            }

            return new Output(Globals.ImportStatus, Globals.LastUpdated, tuin, list);
        }

    }
}
