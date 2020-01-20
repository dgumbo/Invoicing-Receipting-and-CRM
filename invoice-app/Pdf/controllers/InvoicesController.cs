using Microsoft.AspNetCore.Mvc;
using invoice_demo_app.Pdf.models;
using invoice_demo_app.invoice.service;
using invoice_app.Basic.controllers;
using invoice_demo_app.Basic.services;  
using System.IO;
using PdfSharp.Pdf;

namespace invoice_app.Pdf.controllers
{
    [Route("api/invoices")]
    public class InvoiceController : IBaseController<Invoice>
    {
        private readonly InvoiceService invoiceService;
        private readonly string filename = "0101-single-invoice.pdf";

        public InvoiceController(InvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
        }

        public override IBasicService<Invoice> GetService()
        {
            return invoiceService;
        }


        [HttpGet("view/invoice")] //using Path Variable 
        public ActionResult<byte[]> viewFile() //throws IOException
        {
            // System.out.println("zw.co.hisolutions.storage.controllers.StorageController.viewFile()");
            return disposeFileContent(filename); 
        }

        private ActionResult<byte[]> disposeFileContent(string filename) //, ContentDisposalType contentDisposalType) //throws IOException
        {
            Invoice invoice = invoiceService.FindAllEntities()[0];

            PdfDocument document = invoiceService.PrintDocument(invoice);

            MemoryStream stream = new MemoryStream(); 
            document.Save(stream);  

            byte[] byteArray = stream.  ToArray();
            MemoryStream pdfStream = new MemoryStream();
            pdfStream.Write(byteArray, 0, byteArray.Length);
            pdfStream.Position = 0;
            return new FileStreamResult(pdfStream, "application/pdf"); 
        }
    }
}
