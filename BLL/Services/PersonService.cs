using CsvHelper;
using CsvHelper.Configuration;
using DAL;
using DAL.Entities;
using Microsoft.VisualBasic.FileIO;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BLL.Services
{
    public class PersonService
    { 

        public async Task<List<Person>> LoadExcelFile(FileInfo file)
        {
            List<Person> people = new();      
            using var package = new ExcelPackage(file);
            await package.LoadAsync(file);
            var ws = package.Workbook.Worksheets[0];
            int row = 2;
            int col = 1;
            while (string.IsNullOrWhiteSpace(ws.Cells[row,col].Value?.ToString())== false)
            {
                List<Pet> pets = new();
                Person p = new();
                p.PersonName = ws.Cells[row, col].Value.ToString();
                p.Age =int.Parse(ws.Cells[row, col+1].Value.ToString());
                int colp = 1;
                while (colp < 7)
                {
                    Pet pet = new();
                    pet.Name = ws.Cells[row, colp + 2].Value.ToString();
                    pet.Type = ws.Cells[row, colp + 3].Value.ToString();
                    
                    pets.Add(pet);
                    if (pet.Name == "-")
                        pets.Remove(pet);
                    colp += 2;
                    p.Pets = pets;
                }
                people.Add(p);
                row += 1;
            }
            return people;
        }
        public async Task<List<Person>> LoadCsvFile(string path)
        {
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                Comment = '#'
            };
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, configuration))
            {
                csv.Context.RegisterClassMap<PersonMap>();
                var records = csv.GetRecords<Person>();
                var listRecords = records.ToList();
                return listRecords;
            }
        }
    }
}
