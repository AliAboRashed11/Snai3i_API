using AutoMapper;
using Snai3i_BLL.DTO;
using Snai3i_BLL.DTO.WorkerDto;
using Snai3i_DAL.Data.Repository.WorkerRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.Manager.WorkerManager
{
    public class workermanager : IworkerManager
    {
        private readonly IworkerRepository workerrepository;

        private readonly IMapper _mapper;

        public workermanager(IworkerRepository workerrepository , IMapper Mapper)
        {
            this.workerrepository = workerrepository;
            _mapper = Mapper; 
        }

        public async Task<IEnumerable<WorkerReadDto>> GetWorkerByCraft(int id)
        {
            return _mapper.Map<List<WorkerReadDto>>(await workerrepository.GetAllWorkByCraft(id));
        }
        public async Task<WorkerDataDto> GetAllWorkers(WorkerParamters workerParamters)
        {
            var workerpagedlist = await workerrepository.GetAllWorkers(workerParamters); 

            var workerdata = new WorkerDataDto();

            workerdata.workerReadList =
                _mapper.Map<List<WorkerReadDto>>(workerpagedlist) ;

            workerdata.data = new MetaDataDto()
            {
                CurrentPage = workerpagedlist.CurrentPage,
                TotalCount = workerpagedlist.TotalCount,
                HasNextPage = workerpagedlist.HasNextPage,
                HasPreviousPage = workerpagedlist.HasPreviousPage,  
                pageSize = workerpagedlist.pageSize,
                TotalPages = workerpagedlist.TotalPages,    
            };


            return workerdata;

        }
    }
}
