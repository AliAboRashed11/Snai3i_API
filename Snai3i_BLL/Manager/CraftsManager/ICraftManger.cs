using Snai3i_BLL.DTO.CraftDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.Manager.CraftsManager
{
    public interface ICraftManger
    {

        public Task<IEnumerable<CraftReadDTO>> GetAllCraftAsync();

        public Task<CraftReadDTO> GetCraftByIdAsync(int id);

        public Task AddCraftAsync(CraftAddDTO craft);

        public Task EidetCraftAsync(CraftUdateDTO craft);

        public Task DeleteCraftAsync(int id);

    }
}
