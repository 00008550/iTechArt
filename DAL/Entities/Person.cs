using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DAL.Entities
{
    public class Person
    {
        public Guid Id { get; set; }
        [Name("PersonName")]
        public string PersonName { get; set; }
        [Name("Age")]
        public int Age { get; set; }
        [XmlIgnore]
        public ICollection<Pet> Pets { get; set; }
    }
}
