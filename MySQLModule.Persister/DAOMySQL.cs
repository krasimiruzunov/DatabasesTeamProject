using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySQLModule.Model;
using Telerik.OpenAccess;
using Telerik.OpenAccess.API;
using ProductInfo;

namespace MySQLModule.Persister
{
    public class DAOMySQL
    {
        static void Main()
        {
            List<ProductInformation> list = GetData();
            Console.WriteLine(list);
        }

        public static List<VendorInformation> GetVendors()
        {
            EntitiesModel superMarketDb = new EntitiesModel();
            var test = (from v in superMarketDb.Vendors
                        select new VendorInformation()
                        {
                            VendorId = v.IdVendors,
                            VendorName = v.VendorName
                        }).ToList();
            return test;
        }

        public static List<MeasureInformation> GetMeasures()
        {
            EntitiesModel superMarketDb = new EntitiesModel();
            var test = (from m in superMarketDb.Measures
                        select new MeasureInformation()
                        {
                            MeasureId = m.IdMeasures,
                            MeasureName = m.MeasureName
                        }).ToList();
            return test;
        }

        public static List<ProductInformation> GetData()
        {
            EntitiesModel superMarketDb = new EntitiesModel();
            var test = (from p in superMarketDb.Products
                        select new ProductInformation()
                        {
                            ProductName = p.ProductName,
                            MeasureId = p.Measure.IdMeasures,
                            VendorId = p.Vendor.IdVendors,
                            BasePrice = p.BasePrice
                        }).ToList();

            return test;

            ////Console.WriteLine(test);
            //foreach (var item in test)
            //{
            //    Console.WriteLine("--------------------------------------");
            //    Console.WriteLine(item.BasePrice);
            //    Console.WriteLine(item.Measure);
            //    Console.WriteLine(item.ProductName);
            //    Console.WriteLine(item.VendorName);
            //    Console.WriteLine("--------------------------------------");
            //}
        }


    }
}
