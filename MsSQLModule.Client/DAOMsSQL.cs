using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MsSQLModule.Data;
using MsSQLModule.Data.Migrations;
using System.Data.Entity;
using MsSQLModule.Model;
using ProductInfo;
using SalesInformation;

namespace MsSQLModule.Persister
{
    public class DAOMsSQL
    {
        public static void SetData(
            List<VendorInformation> vendors,
            List<MeasureInformation> measures,
            List<ProductInformation> products)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SqlServerEntities, Configuration>());
            using (var db = new SqlServerEntities())
            {
                foreach (var vend in vendors)
                {
                    db.Vendors.Add(new Vendor 
                    {
                        VendorId = vend.VendorId,
                        VendorName = vend.VendorName 
                    });
                }

                foreach (var meas in measures)
                {
                    db.Measures.Add(new Measure
                    {
                        MeasureId = meas.MeasureId,
                        MeasureName = meas.MeasureName
                    });
                }

                foreach (var prod in products)
                {
                    db.Products.Add(new Product 
                    { 
                        ProductName = prod.ProductName,
                        BasePrice = (decimal)prod.BasePrice,
                        MeasureId = prod.MeasureId,
                        VendorId = prod.VendorId 
                    });
                }

                db.SaveChanges();
            }
        }

        public static void SetDataFromXls(List<List<SaleInfo>> reports)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SqlServerEntities, Configuration>());
            using (var db = new SqlServerEntities())
            {
                foreach (var file in reports)
                {
                    foreach (var rep in file)
                    {
                        var supermarket = db.Supermarkets.FirstOrDefault(x => x.SupermarketName == rep.Location);

                        if (supermarket == null)
                        {
                            supermarket = new Supermarket { SupermarketName = rep.Location };
                            db.Supermarkets.Add(supermarket);
                        }
                        
                        var sale = new Sale
                        {
                            ProductId = rep.ProductId,
                            Supermarket = supermarket,
                            Date = rep.SaleDate,
                            Quantity = rep.Quantity,
                            UnitPrice = rep.UnitPrice,
                            Sum = rep.Sum
                        };

                        db.Sales.Add(sale);
                        db.SaveChanges();
                    }

                }
                
               
            }
        }

        public static List<XMLReportInfo> XMLPersiter()
        {
            List<XMLReportInfo> info = new List<XMLReportInfo>();
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SqlServerEntities, Configuration>());
            using (var db = new SqlServerEntities())
            {
                  var salesReports = (from s in db.Sales
                                    group s by s.Product.Vendor.VendorName).ToList();

                  foreach (var report in salesReports)
                  {
                      XMLReportInfo curReport = new XMLReportInfo();
                      curReport.Key =  report.Key;
                      List<decimal> sums = new List<decimal>();
                          List<DateTime> dates = new List<DateTime>();
                      foreach (var item in report)
                      {
                          dates.Add(item.Date);
                          sums.Add(item.Sum);
                      }
                      curReport.Sums = sums;
                      curReport.Dates = dates;
                      info.Add(curReport);
                  }
                
            }
            return info;
        }

      
    }
}
