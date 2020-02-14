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
        PdfDocument PrintDocument(Invoice invoice); 
    }
}
