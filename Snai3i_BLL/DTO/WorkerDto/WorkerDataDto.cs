using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.DTO.WorkerDto
{
    public class WorkerDataDto
    {
        public MetaDataDto data { get; set; }

        public List<WorkerReadDto> workerReadList { get; set; }
    }
}
