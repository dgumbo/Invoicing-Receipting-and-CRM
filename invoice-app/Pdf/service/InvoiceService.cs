using invoice_demo_app.Basic.services;
using invoice_demo_app.Pdf.models;
using PdfSharp.Pdf;
/**
*
* @author dgumbo
*/
namespace invoice_demo_app.invoice.service
{
    public interface InvoiceService : IBasicService<Invoice>  //where T: BaseEntity
    { 
       // PDDocument PrintPDF(Invoice invoice);

        PdfDocument PrintDocument(Invoice invoice);

        //string GetMimeType(Resource file); ;

        //string GetMimeType(ByteArrayInputStream in) ; ;
    }
}
