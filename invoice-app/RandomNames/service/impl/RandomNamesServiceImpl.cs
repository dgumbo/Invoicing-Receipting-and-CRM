using invoice_app.RandomNames.models;
using Microsoft.Office.Interop.Excel;
using RandomNameGeneratorLibrary;
using System;
using System.Collections.Generic;
using System.Linq; 

namespace invoice_app.RandomNames.service.impl
{
    public class RandomNamesServiceImpl : RandomNamesService
    {
        private readonly PersonNameGenerator personGenerator = new PersonNameGenerator();

        public Workbook ExportToExcel(List<RandomPerson> randomNamesDT)
        {
            throw new NotImplementedException();
        }

        public /* System.Data.DataTable*/ List<RandomPerson> generateRandomFullnames(int iCount)
        {
            IEnumerable<string> NamesList = personGenerator.GenerateMultipleFirstAndLastNames(iCount);

            List<RandomPerson> personList = new List<RandomPerson>();
            NamesList.ToList().ForEach(sFullname =>
            {
                RandomPerson person = GetPersonFromString(sFullname);
                if (person != null)
                    personList.Add(person);
            });
            // Console.WriteLine(NamesList); //Outputs some random first and last name combination in the format "{first} {last}
            return personList;
        }

        private RandomPerson GetPersonFromString(string sFullname)
        {
            int iSpacePos = sFullname.IndexOf(" ");
            int iStrLength = sFullname.Length;

            if (sFullname.Length > 5 && iSpacePos > 1 && iSpacePos < iStrLength - 1)
            {
                string firstName = sFullname.Substring(0, iSpacePos).Trim();
                string middleName = "";
                string lastName = sFullname.Substring(iSpacePos, iStrLength - iSpacePos).Trim();

                return new RandomPerson() { Firstname = firstName, Middlename = middleName, Lastname = lastName };
            }

            return null;
        }
    }
}
