using invoice_app.RandomNames.service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks; 
using Microsoft.Office.Interop.Excel ;
using System.IO;
using invoice_app.RandomNames.models;

namespace invoice_app.Controllers
{
    [Route("api/random")]
    public class RandomNamesGeneratorController : ControllerBase
    {
        private readonly RandomNamesService randomNamesService;

        public RandomNamesGeneratorController(RandomNamesService randomNamesService)
        {
            this.randomNamesService = randomNamesService;
        }

        [HttpGet("get-data")]
        public ActionResult<byte[]> InitTestInvoice()
        {
            List< RandomPerson> randomNamesDT = randomNamesService.generateRandomFullnames(1000000);

            byte[] dataAsBytes = randomNamesDT.SelectMany(s =>
            System.Text.Encoding.UTF8.GetBytes(s.Firstname + " " + /*s.Middlename + " " +*/ s.Lastname + Environment.NewLine)).ToArray();

            //Workbook randomNamesWorkbook = randomNamesService.ExportToExcel(randomNamesDT);

            //Console.Out.WriteLine("[HttpPost]"); 

            //// Get temp file name
            //var temp = Path.GetTempPath(); // Get %TEMP% path
            //var file = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()); // Get random file name without extension
            //var path = Path.Combine(temp, file + ".xlsx"); // Get random file path

            //MemoryStream stream = new MemoryStream();
            //randomNamesWorkbook. SaveAs(stream);

            //byte[] byteArray = stream.ToArray();

            MemoryStream pdfStream = new MemoryStream();
            pdfStream.Write(dataAsBytes, 0, dataAsBytes.Length);
            pdfStream.Position = 0;

            return new FileStreamResult(pdfStream, "application/text");
        }
    }
}
