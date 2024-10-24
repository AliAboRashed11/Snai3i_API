using Snai3i_BLL.DTO.SizesDTO;
using Snai3i_BLL.DTO.ToolsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.Manager.SizesManager
{
    public interface ISizeManager
    {

        //Task<IEnumerable<ReadSizeDTO>> GetAllAsync();

        //Task<ReadSizeDTO> GetByIdAsync(int id);

        public Task AddAsync(AddSizeDTO addSizeDTO);

        public Task UpdateAsync(UpdateSizeDTO updateSizeDTO);

        public Task DeleteAsync(int id);

    }
}
