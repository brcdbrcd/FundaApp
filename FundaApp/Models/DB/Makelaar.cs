using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundaApp.Models.DB
{
    public class Makelaar
    {
        [BsonId]
        public ObjectId InternalId { get; set; }

        public int MakelaarId { get; set; }
        
        public String MakelaarName { get; set; }

        public int HouseCount { get; set; }

        public int HouseWithGardenCount { get; set; }
    }
}
