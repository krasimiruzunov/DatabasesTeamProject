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
using MsSQLModule.Data;
using MsSQLModule.Model;
using System.IO;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
namespace MongoDbModule
{
    public class MongoWorker
    {
        public static void SaveData()
        {
            var mongoClient = new MongoClient(@"mongodb://localhost/");
            var mongoServer = mongoClient.GetServer();
            var productsDb = mongoServer.GetDatabase("ProductsDb");
            var products = productsDb.GetCollection<FullIProductInformation>("products");
            SqlServerEntities db = new SqlServerEntities();
            using (db)
            {
                var salesReports = db.Sales
                                         .Include("Product")
                                         .Include("Supermarket")
                                         .Include("Product.Vendor")
                                         .ToList()
                                         .GroupBy(x => x.Product.ProductName);

                foreach (var item in salesReports)
                {
                    FullIProductInformation info = new FullIProductInformation();

                    info.TotalIncomes = item.Sum(x => x.Sum);
                    info.ProductName = item.Key;
                    info.TotalQuantitySold = item.Sum(x => x.Quantity);
                    info.VendorName = item.First().Product.Vendor.VendorName;
                    info.ProductId = item.First().ProductId;
                    SaveToFile(info);
                    products.Insert(info);
                }

            }

        }

        private static void SaveToFile(FullIProductInformation product)
        {
            var json = new JavaScriptSerializer().Serialize(product);
            string path = @"..\..\Products-reports\";
            StreamWriter writer = new StreamWriter(path + product.ProductId + ".json");

            using (writer)
            {
                writer.Write(json);
            }
        }

        public static List<FullIProductInformation> getData()
        {
            var mongoClient = new MongoClient(@"mongodb://localhost/");
            var mongoServer = mongoClient.GetServer();
            var productsDb = mongoServer.GetDatabase("ProductsDb");
            var products = productsDb.GetCollection<FullIProductInformation>("products");
            var res = products.FindAll().ToList();
            return res;
        }
    }
}
