using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Ionic.Zip;
using System.Data.OleDb;
using SalesInformation;
using System.Globalization;

namespace ExcelReaderModule
{


    public class ExcelReader
    {

        public static void MyExtract()
        {
            string zipToUnpack = @"..\..\..\Sample-Sales-Reports.zip";
            string unpackDirectory = @"..\..\Extracted Files";
            using (ZipFile zip1 = ZipFile.Read(zipToUnpack))
            {
                // here, we extract every entry, but we could extract conditionally
                // based on entry name, size, date, checkbox status, etc.  
                foreach (ZipEntry e in zip1)
                {
                    e.Extract(unpackDirectory, ExtractExistingFileAction.OverwriteSilently);
                }
            }

        }

        public static List<List<SaleInfo>> ReadAllExcells()
        {
            List<List<SaleInfo>> data = new List<List<SaleInfo>>();
            string firstConStringPart = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=";
            string secondConStringPart = @";Extended Properties='Excel 12.0 Xml;HDR=YES'";
            //IF time DFS
            string[] fileNames = { @"..\..\Extracted Files\20-Jul-2013\Bourgas-Plaza-Sales-Report-20-Jul-2013.xls",
                                     @"..\..\Extracted Files\20-Jul-2013\Kaspichan-Center-Sales-Report-20-Jul-2013.xls",
                                     @"..\..\Extracted Files\20-Jul-2013\Zmeyovo-Sales-Report-20-Jul-2013.xls",
                                    @"..\..\Extracted Files\21-Jul-2013\Zmeyovo-Sales-Report-21-Jul-2013.xls",
                                    @"..\..\Extracted Files\21-Jul-2013\Bourgas-Plaza-Sales-Report-21-Jul-2013.xls",
                                    @"..\..\Extracted Files\21-Jul-2013\Kaspichan-Center-Sales-Report-21-Jul-2013.xls",
                                     @"..\..\Extracted Files\21-Jul-2013\Plovdiv-Stolipinovo-Sales-Report-21-Jul-2013.xls",
                                      @"..\..\Extracted Files\22-Jul-2013\Bourgas-Plaza-Sales-Report-22-Jul-2013.xls",
                                     @"..\..\Extracted Files\22-Jul-2013\Kaspichan-Center-Sales-Report-22-Jul-2013.xls",
                                     @"..\..\Extracted Files\22-Jul-2013\Plovdiv-Stolipinovo-Sales-Report-22-Jul-2013.xls",

                                    };
            //Console.WriteLine(firstConStringPart + fileNames[0] + secondConStringPart);


            foreach (var path in fileNames)
            {
                data.Add(ReadCurrentExcel(firstConStringPart + path + secondConStringPart));
            }
            return data;
        }

        private static List<SaleInfo> ReadCurrentExcel(string path)
        {
            List<SaleInfo> allSales = new List<SaleInfo>();
            using (OleDbConnection conn = new OleDbConnection(path))
            {
                
                conn.Open();
                string command = @"select * from [Sales$]";
                OleDbDataAdapter adapter = new OleDbDataAdapter(command, conn);
                DataTable table = new DataTable();
                using (adapter)
                {
                    adapter.FillSchema(table, SchemaType.Source);
                    adapter.Fill(table);
                }
                
                int counter = 0;
                string[] splittedName = path.Split(new string[] { "-Sales-Report-" }, StringSplitOptions.RemoveEmptyEntries);
                string location = splittedName[0].Substring(splittedName[0].LastIndexOf('\\'));
                int dotIndex = splittedName[1].IndexOf('.');
                string date = splittedName[1].Substring(0, dotIndex);
                foreach (DataRow row in table.Rows)
                {
                    SaleInfo currentSale = new SaleInfo();
                    List<decimal> info = new List<decimal>();
                    bool getInside = false;
                    
                    foreach (DataColumn  col in table.Columns)
                    {
                        if (row[col].ToString()!="")
                        {
                            info.Add(decimal.Parse(row[col].ToString()));
                           // Console.Write(row[col].ToString() + " ");
                            getInside = true;
                        }
                        
                    }

                    if (getInside && counter != table.Rows.Count-1)
                    {
                        currentSale.ProductId = (int)info[0];
                        currentSale.Quantity = (int)info[1];
                        currentSale.UnitPrice = info[2];
                        currentSale.Sum = info[3];
                        currentSale.Location = location;
                        currentSale.SaleDate = DateTime.ParseExact(date, "dd-MMM-yyyy", CultureInfo.InvariantCulture);
                        allSales.Add(currentSale); 
                    }
                    counter++;
                }

            }

            return allSales;

        }



    }
}
