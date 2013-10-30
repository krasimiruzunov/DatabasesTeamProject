using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using SalesReportEntities;

namespace GeneratePDF
{
    class Program
    {
        static void Main()
        {
            Document myDocument = new Document(PageSize.A4.Rotate());

            try
            {
                PdfWriter.GetInstance(myDocument, new FileStream("..\\..\\salesReport.pdf", FileMode.Create));
                PdfPTable table = new PdfPTable(5);
                float[] widths = new float[] { 25f, 10f, 10f, 45f, 10f };
                table.SetWidths(widths);
                table.DefaultCell.FixedHeight = 27f;
                table.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                myDocument.Open();
                SaleReportsDBEntities context = new SaleReportsDBEntities();
                var sales = from sale in context.Sales.Include("Product").Include("Supermarket").GroupBy(sale => sale.Date)
                            select sale;
                decimal totalSum = 0;
                myDocument.Open();
                var headerFont = FontFactory.GetFont("Arial", 14, Font.BOLD);
                PdfPCell header = new PdfPCell(new Phrase("Aggregate Sales Report", headerFont));
                header.Colspan = 5;
                header.FixedHeight = 27f;
                header.HorizontalAlignment = Element.ALIGN_CENTER;
                header.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(header);
                foreach (var sale in sales)
                {

                    decimal currentSum = 0;
                    var font = FontFactory.GetFont("Arial", 14, Font.BOLD);
                    PdfPCell date = new PdfPCell(new Phrase("Date: " + (sale.Key.Date).ToString("dd-MM-yyyy"), font));
                    date.Colspan = 5;
                    date.FixedHeight = 27f;
                    date.BackgroundColor = new Color(210, 210, 210);
                    date.HorizontalAlignment = Element.ALIGN_LEFT;
                    date.VerticalAlignment = Element.ALIGN_MIDDLE;
                    table.AddCell(date);
                    PdfPCell product = new PdfPCell(new Phrase("Product", font));
                    product.FixedHeight = 27f;
                    product.BackgroundColor = new Color(210, 210, 210);
                    product.HorizontalAlignment = Element.ALIGN_LEFT;
                    product.VerticalAlignment = Element.ALIGN_MIDDLE;
                    table.AddCell(product);
                    PdfPCell quantity = new PdfPCell(new Phrase("Quantity", font));
                    quantity.FixedHeight = 27f;
                    quantity.BackgroundColor = new Color(210, 210, 210);
                    quantity.HorizontalAlignment = Element.ALIGN_LEFT;
                    quantity.VerticalAlignment = Element.ALIGN_MIDDLE;
                    table.AddCell(quantity);
                    PdfPCell price = new PdfPCell(new Phrase("Unit Price", font));
                    price.FixedHeight = 27f;
                    price.BackgroundColor = new Color(210, 210, 210);
                    price.HorizontalAlignment = Element.ALIGN_LEFT;
                    price.VerticalAlignment = Element.ALIGN_MIDDLE;
                    table.AddCell(price);
                    PdfPCell location = new PdfPCell(new Phrase("Location", font));
                    location.FixedHeight = 27f;
                    location.BackgroundColor = new Color(210, 210, 210);
                    location.HorizontalAlignment = Element.ALIGN_LEFT;
                    location.VerticalAlignment = Element.ALIGN_MIDDLE;
                    table.AddCell(location);
                    PdfPCell sumCol = new PdfPCell(new Phrase("Sum", font));
                    sumCol.FixedHeight = 27f;
                    sumCol.BackgroundColor = new Color(210, 210, 210);
                    sumCol.HorizontalAlignment = Element.ALIGN_LEFT;
                    sumCol.VerticalAlignment = Element.ALIGN_MIDDLE;
                    table.AddCell(sumCol);
                    foreach (var item in sale)
                    {
                        table.AddCell((item.Product.ProductName).ToString());
                        table.AddCell((item.Quantity).ToString() + " " + item.Product.Measure.MeasureName);
                        table.AddCell((item.UnitPrice).ToString());
                        table.AddCell(String.Format("Supermarket \"{0}\"", item.Supermarket.SupermarketName));
                        PdfPCell sum = new PdfPCell(new Phrase((item.Sum).ToString()));
                        sum.HorizontalAlignment = Element.ALIGN_RIGHT;
                        table.AddCell(sum);
                        currentSum += item.Sum;
                        totalSum += item.Sum;
                    }
                    PdfPCell footerDate = new PdfPCell(new Phrase("Total sum for " + (sale.Key.Date).ToString("dd-MM-yyyy") + ": "));
                    footerDate.Colspan = 4;
                    footerDate.FixedHeight = 27f;
                    footerDate.HorizontalAlignment = Element.ALIGN_RIGHT;
                    footerDate.VerticalAlignment = Element.ALIGN_MIDDLE;
                    table.AddCell(footerDate);
                    PdfPCell current = new PdfPCell(new Phrase(currentSum.ToString()));
                    current.FixedHeight = 27f;
                    current.HorizontalAlignment = Element.ALIGN_RIGHT;
                    current.VerticalAlignment = Element.ALIGN_MIDDLE;
                    table.AddCell(current);
                }
                PdfPCell grandTotal = new PdfPCell(new Phrase("Grand total: "));
                grandTotal.Colspan = 4;
                grandTotal.FixedHeight = 27f;
                grandTotal.BackgroundColor = new Color(210, 210, 210);
                grandTotal.HorizontalAlignment = Element.ALIGN_RIGHT;
                grandTotal.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(grandTotal);
                PdfPCell total = new PdfPCell(new Phrase(totalSum.ToString()));
                total.BackgroundColor = new Color(210, 210, 210);
                total.HorizontalAlignment = Element.ALIGN_RIGHT;
                total.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(total);
                myDocument.Add(table);
            }
            catch (DocumentException de)
            {
                Console.WriteLine(de.Message);
            }
            catch (IOException ioe)
            {
                Console.WriteLine(ioe.Message);
            }

            myDocument.Close();
        }
    }
}
