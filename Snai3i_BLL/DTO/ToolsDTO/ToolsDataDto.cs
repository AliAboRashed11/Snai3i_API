using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.DTO.ToolsDTO
{
    public class ToolsDataDto
    {
        public ToolsMetaDataDto data { get; set; }

        public List<ReadToolDTO> toolsReadList { get; set; }
    }
}
