using Snai3i_BLL.DTO.ReviewDto;
using Snai3i_BLL.DTO.SalesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.Manager.SalessManager
{
    public interface ISalesManager
    {
        public Task AddSalesAsync(SalesAddDTO salesAddDTO);
        Task<IEnumerable<SalesReadDTO>> GetAllSalesAsync();
        public Task UpdateAsync(SalesUpdateDTO salesUpdateDTO);
        Task<SalesReadDTO> GetByIdAsync(int id);

        public Task DeleteAsync(int id);
    }
}
