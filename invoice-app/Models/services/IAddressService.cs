using invoice_demo_app.Basic.services;
using invoice_demo_app.Models;
using invoice_demo_app.Pdf.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace invoice_app.Services
{
    public interface IAddressService : IBasicService<Address>
    {
    }
}
