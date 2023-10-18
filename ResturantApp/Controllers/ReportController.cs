using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Mvc;
using RestaurantApp.BL.interfaces;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using iText.Html2pdf;
using iText.IO.Source;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using NuGet.ProjectModel;
using DocumentFormat.OpenXml.Office.ContentType;
using System.Data.SqlTypes;
using System.Reflection.Metadata;
using Humanizer;
using System.Text;
using DocumentFormat.OpenXml.Wordprocessing;

namespace ResturantApp.Controllers
{
    public class ReportController : Controller
    {
        private readonly IOrder _order;
        private readonly IProductOrder productOrder;

        public iText.Layout.Borders.Border border { get; private set; }

        public ReportController(IOrder order, IProductOrder productOrder)
        {
            this._order = order;
            this.productOrder = productOrder;
        }
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public JsonResult GetData(int day, int month, int year)
        {
            var data = _order.GetAll().Where(a => a.Date.Day == day && a.Date.Month == month && a.Date.Year == year);
            List<int> total = new List<int>();
            foreach (var item in data)
            {
                total.Add(productOrder.getPrice(item.Id));
            }
            return Json(new { orders = data, total = total });

        }

        public IActionResult Excel(int day, int month, int year)
        {
            var data = _order.GetAll().Where(a => a.Date.Day == day && a.Date.Month == month && a.Date.Year == year);
            using (var ws = new XLWorkbook())
            {
                var worksheet = ws.Worksheets.Add("Report");
                int current_row = 1;
                worksheet.Cell(current_row, 1).Value = "Order Number";
                worksheet.Cell(current_row, 2).Value = "Customer Name";
                worksheet.Cell(current_row, 3).Value = "Customer Email";
                worksheet.Cell(current_row, 4).Value = "Total Price";

                foreach (var item in data)
                {
                    current_row++;
                    worksheet.Cell(current_row, 1).Value = item.Id;
                    worksheet.Cell(current_row, 2).Value = item.customer.Name;
                    worksheet.Cell(current_row, 3).Value = item.customer.Email;
                    worksheet.Cell(current_row, 4).Value = productOrder.getPrice(item.Id);

                }

                using (var stream = new MemoryStream())
                {
                    ws.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(
                       content,
                       "application/pdf",
                       "orders for " + day + "/" + month + "/" + year + ".xlsx"
                       );
                }

            }
        }
        public IActionResult pdf(string html, int day, int month, int year)
        {
          
            using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(html)))
            {
             

                ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
                PdfWriter writer = new PdfWriter(byteArrayOutputStream);
              
                PdfDocument pdfDocument = new PdfDocument(writer);
              
                pdfDocument.SetDefaultPageSize(iText.Kernel.Geom.PageSize.A4);
       

                HtmlConverter.ConvertToPdf(stream, pdfDocument);

                pdfDocument.Close();
                return File(byteArrayOutputStream.ToArray(), "application/pdf", "orders for "+day+'/'+month+'/'+year+".pdf");
            }
        }
    }
}
