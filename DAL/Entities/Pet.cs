using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Pet
    {
        public Guid Id { get; set; }
        [Name("Pet 1","Pet 2","Pet 3")]
        public string Name { get; set; }
        [Name("Pet 1 Type", "Pet 2 Type", "Pet 3 Type")]
        public string Type { get; set; }
    }
}
