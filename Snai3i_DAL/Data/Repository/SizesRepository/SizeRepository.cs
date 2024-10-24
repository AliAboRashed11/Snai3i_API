using Snai3i_DAL.Data.Context;
using Snai3i_DAL.Data.Models;
using Snai3i_DAL.Data.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Repository.SizesRepository
{
    public class SizeRepository : GenericRepository<Size>, ISizeRepository
    {
        public SizeRepository(SnaiiDBContext _context) : base(_context)
        {

        }


    }
}
