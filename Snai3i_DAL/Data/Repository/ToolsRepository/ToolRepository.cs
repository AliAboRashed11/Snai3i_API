using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Snai3i_BLL.DTO.WorkerDto;
using Snai3i_DAL.Data.Context;
using Snai3i_DAL.Data.Models;
using Snai3i_DAL.Data.Repository.GenericRepository;
using Snai3i_DAL.Data.service.FileService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Repository.ToolsRepository
{
    public class ToolRepository : GenericRepository<Tool>, IToolRepository
    {
        private readonly SnaiiDBContext context;
        private readonly Ifileservice fieservice;

        public ToolRepository(SnaiiDBContext _context) : base(_context)
        {
            context = _context;
        }
        public async Task<IEnumerable<Tool>> GetSomeToolsAsync()
        {

            return await context.tools.Take(4).Include(a => a.sizes).AsNoTracking().ToListAsync();
        }
        // generic get all async
        public async Task<IEnumerable<Tool>> GetAllincAsync()
        {

            return await context.tools.Include(a => a.sizes).AsNoTracking().ToListAsync();
        }


       public async Task<Tool> GetByIdincAsync(int id)
        {
            return await context.tools.Include(a => a.sizes).FirstOrDefaultAsync(a=>a.Id==id);
        }
        
        public async Task<PagedList<Tool>> GetAllTools(ToolParamters toolparams)
        {


            return await PagedList<Tool>.ToPagedList(Context.tools.
                Include(a => a.sizes).
                OrderBy(t=>t.Name).AsNoTracking()
                , toolparams.pagesize,
                toolparams.PageNumber);
        }
    }
}
