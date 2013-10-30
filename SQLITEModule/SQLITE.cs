using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDbModule;
using MsSQLModule.Data;
using MsSQLModule.Model;
using System.Data.OleDb;
namespace SQLITEModule
{
    public class SQLITE
    {
        public static void GenerateTaxes()
        {
            using (SqlServerEntities db = new SqlServerEntities())
            {
                var names = from p in db.Products
                            select new
                            {
                                Product = p.ProductName,
                                Id = p.ProductId
                            };
                using (VendorsDB dbLite = new VendorsDB())
                {
                    Random rnd = new Random();
                    foreach (var item in names)
                    {
                        dbLite.Taxes.Add(new Tax() { Id = item.Id, ProductName = item.Product, Tax1 = rnd.Next(1, 40) });
                    }
                    dbLite.SaveChanges();
                }
            }
        }

        public static void FillSQLITEProducts()
        {
            List<FullIProductInformation> info = MongoWorker.getData();
            using (VendorsDB dbLite = new VendorsDB())
            {
                foreach (var item in info)
                {
                    dbLite.Products.Add(new Product()
                    {
                        productName = item.ProductName,
                        ProductID = item.ProductId,
                        vendorName = item.VendorName,
                        totalIncomes = item.TotalIncomes,
                        totalQuantitySold = item.TotalQuantitySold
                    });
                }
                dbLite.SaveChanges();
            }

        }
        public static void FillExcelFile()
        {
            string CONNECTION_STRING = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=G:\DataBases\teamwork\New\SQLITEModule\reports.xlsx;
                                                     Extended Properties='Excel 12.0'";
            using (VendorsDB db = new VendorsDB())
            {
                var info = (from p in db.Products.Include("Tax")
                            //join t in db.Taxes on p.productName equals t.ProductName
                            select new
                            {
                                VendorName = p.vendorName,
                                ProductName = p.productName,
                                tax = p.Tax.Tax1,
                                Incomes = p.totalIncomes
                            }
                                ).ToList().GroupBy(x => x.VendorName);
                OleDbConnection excolCon = new OleDbConnection(CONNECTION_STRING);
                using (excolCon)
                {
                    Random rnd = new Random();
                    excolCon.Open();

                    foreach (var item in info)
                    {
                        foreach (var x in item)
                        {
                            string command = @"INSERT INTO [Sheet1$] 
                                             VALUES (@vendor)";//,@income)";//,@expences,@taxes,@result)";
                            OleDbCommand cmd = new OleDbCommand(command, excolCon);
                            cmd.Parameters.AddWithValue("@vendor", x.VendorName);
                           // cmd.Parameters.AddWithValue("@income", x.Incomes.ToString());
                            //cmd.Parameters.AddWithValue("@expences", rnd.Next(100, 200).ToString());
                            //cmd.Parameters.AddWithValue("@taxes", 100.ToString());
                            //cmd.Parameters.AddWithValue("@result", 200.ToString());//(x.Incomes -x.Incomes*(x.tax/100))
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
             
                //var t = from p in db.Products
                //        select p;
                //Console.WriteLine(t);

            }

        }

    }
}
