using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Snai3i_BLL.DTO.WorkerDto;
using Snai3i_DAL.Data.Context;
using Snai3i_DAL.Data.Models;
using Snai3i_DAL.Data.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Repository.WorkerRepo
{
    public class WorkerRepository : GenericRepository<Worker> , IworkerRepository
    {
        public WorkerRepository(SnaiiDBContext context) : base(context) { }
        [Authorize]
        public async Task<PagedList<ApplicationUser>> GetAllWorkers(WorkerParamters workerparams)
        {


            return await PagedList<ApplicationUser>.ToPagedList( Context.applicationUsers.
                
                 
                Where(a => a.GetType() == typeof(Worker))
                , workerparams.pagesize,
                workerparams.PageNumber); 
        }

        public async Task<IEnumerable<Worker>> GetAllWorkByCraft(int Craft)
        {


            var workersWithCraftId = await Context.applicationUsers
                                                  .Where(a => a is Worker)
                                                  .Select(a => (Worker)a)
                                                .Where(a => a.CraftId == Craft).AsNoTracking()
                                                         .ToListAsync();
            return workersWithCraftId;
        }
        //public async Task<IEnumerable<ApplicationUser>> GetWorkerByCraft(int id)
        //{
        //    return await Context.applicationUsers.Where(a => a.GetType() == typeof(Worker));


        //}
    }
}
