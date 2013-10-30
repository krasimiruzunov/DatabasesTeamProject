using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSQLModule.Model
{
    public class Sale
    {
        public int SaleId { get; set; }

        public int SupermarketId { get; set; }
        public Supermarket Supermarket { get; set; }

        public DateTime Date { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Sum { get; set; }
    }
}
