using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Globalization;
using SalesInformation;


namespace XMLModule
{
    public class XMLReader
    {
        public static void GenerateSalesReportXMLByVendor(List<XMLReportInfo> salesReports)
        {

            var doc = new XDocument();
            var sales = new XElement("sales");

            foreach (var report in salesReports)
            {
                var sale = new XElement("sale", new XAttribute("vendor", report.Key));

                for (int i = 0; i < report.Dates.Count; i++)
                {
                    sale.Add(new XElement("summnary", new XAttribute("date", report.Dates[i].ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture)), new XAttribute("total-sum", report.Sums[i])));
                }

                sales.Add(sale);
            }

            doc.Add(sales);

            doc.Save(@"..\..\SalesReportByDay.xml");
        }

        public static void GenerateSalesReportXMLMontly(List<XMLReportInfo> salesReports)
        {
            var doc = new XDocument();
            var sales = new XElement("sales");

            foreach (var report in salesReports)
            {
                var sale = new XElement("sale", new XAttribute("vendor", report.Key));
                var dates = report.Dates.Distinct().ToList();
                Dictionary<DateTime, decimal> pair = new Dictionary<DateTime, decimal>();
                for (int i = 0; i < dates.Count; i++)
                {
                    pair[dates[i]] = 0;
                }

                for (int i = 0; i < report.Dates.Count; i++)
                {
                    for (int j = 0; j < dates.Count; j++)
                    {
                        if (report.Dates[i] == dates[j])
                        {
                            pair[dates[j]] += report.Sums[i];
                        }
                    }
                }

                var monthExpens = pair.GroupBy(x => x.Key.Month).ToList();
                for (int i = 0; i < monthExpens.Count(); i++)
                {
                    var expense = new XElement("expenses", new XAttribute("month", CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(monthExpens[i].Key).Substring(0,3) + "-2013"));
                    decimal sum = 0;
                    foreach (var item in monthExpens)
                    {
                       
                        foreach (var dailySum in item)
                        {
                            sum += dailySum.Value;
                        }
                    }
                    expense.Value = sum.ToString();
                    sale.Add(expense);
                } 

                sales.Add(sale);
            }

            doc.Add(sales);
           // Console.WriteLine(doc);
            doc.Save(@"..\..\SalesReportByMonth.xml");
        }

    }
}
