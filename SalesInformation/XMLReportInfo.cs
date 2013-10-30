using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInformation
{
  public  class XMLReportInfo
    {
        public string Key { get; set; }
        public List<DateTime> Dates { get; set; }
        public List<decimal> Sums { get; set; }
    }
}
