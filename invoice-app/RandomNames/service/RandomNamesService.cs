using invoice_app.RandomNames.models;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace invoice_app.RandomNames.service
{
    public interface RandomNamesService
    {
        /* System.Data.DataTable*/
        List<RandomPerson> generateRandomFullnames(int Count); 
        Workbook ExportToExcel(List<RandomPerson> randomNamesDT);
    }
}
