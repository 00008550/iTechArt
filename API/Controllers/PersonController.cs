using BLL.Services;
using CsvHelper;
using DAL;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        PersonService personService = new PersonService();
        private readonly IPersonRepository _personRepository;
        private readonly DataContext _context;
        public PersonController(IPersonRepository personRepository, DataContext context)
        {
            _context = context;
            _personRepository = personRepository;
        }
        [HttpPost("ImportXlsx")]
        public async Task<ActionResult<List<Person>>> AddPeopleXlsx()
        {
            var filePath = Path.Combine(TryGetSolutionDirectoryInfo().FullName, "Import Template.xlsx");
            var file = new FileInfo(filePath);
            List<Person> people = await personService.LoadExcelFile(file);
            
            foreach(Person person in people)
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            return Ok(people);

        }
        public static DirectoryInfo TryGetSolutionDirectoryInfo(string currentPath = null)
        {
            var directory = new DirectoryInfo(
                currentPath ?? Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }
            return directory;
        }
        [HttpGet]
        public async Task<List<Person>> GetAll()
        {
            return await _personRepository.GetPeople();
        }
        [HttpPost("ImportCsv")]
        public async Task<ActionResult<List<Person>>> GetCsvPeople()
        {
            var filePath = Path.Combine(TryGetSolutionDirectoryInfo().FullName, "Import Template.csv");
            List<Person> people = await personService.LoadCsvFile(filePath);
            foreach (Person person in people)
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            return Ok(people);
        }
        [HttpPost("ExportXml")]
        public ActionResult ExportXml()
        {

            var list = _context.People.ToList();

            var memory = new MemoryStream();
            var writer = new StreamWriter(memory);
            var serializer = new XmlSerializer(typeof(List<Person>));
            serializer.Serialize(writer, list);
            writer.Flush();

            memory.Position = 0;
            if (memory != Stream.Null)
                return File(memory, "application/xml", $"Export_{DateTime.Now}.xml");

            return NotFound();
        }
    }
}
