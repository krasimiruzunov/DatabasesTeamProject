using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProductInfo
{
    public class ProductInformation
    {
        public int VendorId { get; set; }
        public string ProductName{ get; set; }
        public int MeasureId { get; set; }
        public decimal? BasePrice { get; set; }

        public ProductInformation()
        {
        }

        public ProductInformation(int vendorId,
            string ProductName, int measureId, decimal? price)
        {
            this.VendorId = vendorId;
            this.ProductName = ProductName;
            this.MeasureId = measureId;
            this.BasePrice = price;
        }
    }
}
