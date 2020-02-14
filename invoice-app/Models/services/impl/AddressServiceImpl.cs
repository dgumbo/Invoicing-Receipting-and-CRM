using invoice_demo_app.Models;
using Microsoft.EntityFrameworkCore;

namespace invoice_app.Services.impl
{
    public class AddressServiceImpl : IAddressService
    {
        private readonly AppDbContext appDbContext; 

        public AddressServiceImpl(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public DbSet<Address> GetDbSet()
        {
            return this.appDbContext.Address;
        }

        public AppDbContext GetDbContext()
        {
            return this.appDbContext;
        }
    }
}
