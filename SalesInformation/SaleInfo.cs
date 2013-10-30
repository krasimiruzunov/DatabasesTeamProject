using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInformation
{
    public class SaleInfo
    {
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Sum { get; set; }

        public DateTime SaleDate { get; set; }
        public string Location { get; set; }

    }
}
