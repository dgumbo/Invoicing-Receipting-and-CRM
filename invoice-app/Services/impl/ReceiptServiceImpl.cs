using invoice_demo_app.Pdf.models;
using Microsoft.EntityFrameworkCore;

namespace invoice_app.Services.impl
{
    public class ReceiptServiceImpl : ReceiptService
    {
        private readonly AppDbContext appDbContext;  

        public ReceiptServiceImpl(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public DbSet<Invoice> GetDbSet()
        {
            return this.appDbContext.Invoice;
        }

        public AppDbContext GetDbContext()
        {
            return this.appDbContext;
        }
    }
}
