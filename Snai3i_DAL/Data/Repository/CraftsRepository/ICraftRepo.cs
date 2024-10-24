using Snai3i_DAL.Data.Models;
using Snai3i_DAL.Data.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Repository.CraftsRepository
{
    public interface ICraftRepo:IGenericRepository<Craft>
   {

        

        public Craft GetCraftByIdAsync(int id);
        void Delete(Craft craft);
    }
}
