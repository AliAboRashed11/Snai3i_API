using Microsoft.EntityFrameworkCore;
using Snai3i_DAL.Data.Context;
using Snai3i_DAL.Data.Models;
using Snai3i_DAL.Data.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Repository.CraftsRepository
{
    public class CraftRepo : GenericRepository<Craft>, ICraftRepo
    {

        public CraftRepo(SnaiiDBContext _context) : base(_context)
        {

        }

        public void Delete(Craft craft)
        {
            Context.Remove(craft);
        }

        public Craft GetCraftByIdAsync(int id)
        {
            return  Context.crafts.FirstOrDefault(a => a.CraftId == id);
        }

        //public Task<Craft> GetByIdAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Craft> GetCraftByIdAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
