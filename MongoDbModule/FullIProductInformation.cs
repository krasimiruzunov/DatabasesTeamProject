using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace MongoDbModule
{
   public class FullIProductInformation
    {
        [BsonId]
        public ObjectId _Id { get; set; }

        public string ProductName { get; set; }
        public int  ProductId { get; set; }
        public int TotalQuantitySold{get;set;}
        public decimal TotalIncomes{get;set;}
        public string VendorName { get; set; }
    }
}
