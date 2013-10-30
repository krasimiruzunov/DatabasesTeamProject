using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySQLModule.Persister;
using ProductInfo;
using MsSQLModule.Persister;
using ExcelReaderModule;
using SalesInformation;
using XMLModule;
using MongoDbModule;
using PDFModule;
using SQLITEModule;
namespace MainClient
{
    class Client
    {
        static void Main()
        {
            var vendors = DAOMySQL.GetVendors();
            var measures = DAOMySQL.GetMeasures();
            var products = DAOMySQL.GetData();
            DAOMsSQL.SetData(vendors, measures, products);

            ExcelReader.MyExtract();
            List<List<SaleInfo>> sales = ExcelReader.ReadAllExcells();
            DAOMsSQL.SetDataFromXls(sales);   //1va

            PDFCreator.CreatePDF(); //2ra

            List<XMLReportInfo> info = DAOMsSQL.XMLPersiter();
            XMLReader.GenerateSalesReportXMLByVendor(info); //3ta
            XMLReader.GenerateSalesReportXMLMontly(info); //5ta


            MongoWorker.SaveData();//4ta

            //SQLITE.GenerateTaxes();
            //SQLITE.FillSQLITEProducts();
           // SQLITE.FillExcelFile();
        }
    }
}
