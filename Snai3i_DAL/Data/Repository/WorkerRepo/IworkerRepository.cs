using Snai3i_BLL.DTO.WorkerDto;
using Snai3i_DAL.Data.Models;
using Snai3i_DAL.Data.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Repository.WorkerRepo
{
    public interface IworkerRepository : IGenericRepository<Worker>
    {
        public  Task<PagedList<ApplicationUser>> GetAllWorkers(WorkerParamters workerparams);

        public Task<IEnumerable<Worker>> GetAllWorkByCraft(int Craft);
    }
}
