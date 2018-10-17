using FundaApp.Models.Conf;
using FundaApp.Models.DB;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundaApp.DAO
{
    public class MakelaarContext
    {
        private readonly IMongoDatabase _database = null;

        public MakelaarContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Makelaar> Makelaars
        {
            get
            {
                return GetCollectionWithIndexAsync();
            }
        }

        private IMongoCollection<Makelaar> GetCollectionWithIndexAsync()
        {
            bool collectionFound = _database.ListCollectionNames().ToEnumerable().Contains("Makelaar");

            IMongoCollection<Makelaar> collection = _database.GetCollection<Makelaar>("Makelaar");

            if (!collectionFound)
            {
                Task.Run(() => collection.Indexes.CreateOneAsync(Builders<Makelaar>.IndexKeys.Ascending(item => item.MakelaarId))).Wait();
            }

            return collection;

        }
    }
}
