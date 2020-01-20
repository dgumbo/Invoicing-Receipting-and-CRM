using invoice_demo_app.Models;
using Microsoft.EntityFrameworkCore;

namespace invoice_app.Services.impl
{
    public class ProductServiceImpl : ProductService
    {
        private readonly AppDbContext appDbContext; 

        public ProductServiceImpl(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public DbSet<Product> GetDbSet()
        {
            return this.appDbContext.Product;
        }

        public AppDbContext GetDbContext()
        {
            return this.appDbContext;
        }
    }
}
