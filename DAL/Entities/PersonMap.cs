using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Pet pet = new Pet();
            Map(p => p.PersonName).Index(0);
            Map(p => p.Age).Index(1);
            Map(p => p.Pets).Convert(x =>
            {
                var pets = new List<Pet>();

                for (int i = 2; i < 7; i += 2)
                {
                    var pet = new Pet()
                    {
                        Name = x.Row.GetField<string>(i),
                        Type = x.Row.GetField<string>(i + 1),
                    };
                    pets.Add(pet);
                    if (pet.Name.Contains('-'))
                    {
                        pets.Remove(pet);
                    }
                }
                return pets.ToArray();
            });
        }
    }
}
