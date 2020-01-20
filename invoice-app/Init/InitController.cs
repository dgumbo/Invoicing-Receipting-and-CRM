using System; 
using Microsoft.AspNetCore.Mvc;
using invoice_demo_app.Pdf.models;

namespace invoice_app.Pdf.controllers
{
    [Route("api/init")] 
    public class InitController  : ControllerBase
    { 
        private readonly InitTestData _initTestData;

        public InitController(   InitTestData initTestData )
        {   
            this._initTestData = initTestData;
        }  

        [HttpGet("init-test-data")]
        public ActionResult<Invoice> InitTestInvoice()
        {
            Invoice res = _initTestData.initInvoiceData();
            Console.Out.WriteLine("[HttpPost]");

            return CreatedAtAction("GetInvoice", new { id = res.Id }, res);
        }
    }
}
