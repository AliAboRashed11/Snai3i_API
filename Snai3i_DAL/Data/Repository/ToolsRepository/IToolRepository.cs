using Snai3i_DAL.Data.Models;
using Snai3i_DAL.Data.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Repository.ToolsRepository
{
    public interface IToolRepository:IGenericRepository<Tool>
    {

        ///
        public Task<IEnumerable<Tool>> GetAllincAsync();
        public Task<Tool> GetByIdincAsync(int id);
        public Task<PagedList<Tool>> GetAllTools(ToolParamters toolparams);
        public Task<IEnumerable<Tool>> GetSomeToolsAsync();

    }
}
