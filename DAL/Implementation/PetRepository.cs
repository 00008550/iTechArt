using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementation
{
    internal class PetRepository : IPetRepository
    {
        private readonly DataContext _context;
        public PetRepository(DataContext context)
        {
            _context = context;
        }
    }
}
