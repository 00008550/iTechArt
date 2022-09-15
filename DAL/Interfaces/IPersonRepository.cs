﻿using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IPersonRepository
    {
        Task<List<Person>> GetPeople();
        List<Person> SavePeople(List<Person> people);
    }
}