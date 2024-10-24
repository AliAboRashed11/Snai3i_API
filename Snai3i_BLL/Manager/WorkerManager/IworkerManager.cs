using Snai3i_BLL.DTO.WorkerDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.Manager.WorkerManager
{
    public interface IworkerManager
    {
        public Task<WorkerDataDto> GetAllWorkers( WorkerParamters workerParamters );

        public Task<IEnumerable<WorkerReadDto>> GetWorkerByCraft(int id);
    }
}
