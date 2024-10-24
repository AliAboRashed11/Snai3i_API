using Snai3i_DAL.Data.Models;
using Snai3i_DAL.Data.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Repository.UserReop
{
    public interface IuserRepository : IGenericRepository<User>
    {

        Task<PagedList<ApplicationUser>> GetAllWorkers(UserParameters userparams);
    }
}
