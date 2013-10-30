using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSQLModule.Model
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal BasePrice { get; set; }

        public int MeasureId { get; set; }
        public Measure Measure { get; set; }

        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }
    }
}
