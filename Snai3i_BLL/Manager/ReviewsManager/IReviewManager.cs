using Snai3i_BLL.DTO.ReviewDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.Manager.ReviewsManager
{
    public interface IReviewManager
    {
        public Task AddReviewAsync( ReviewAddDTO reviewAddDTO);
        Task<IEnumerable<ReviewReadDTO>> GetReviewsForWorkerAsync(string workerId);
        public Task UpdateAsync(ReviewUpdateDTO reviewUpdateDTO);
        Task<ReviewReadDTO> GetByIdAsync(int id);

        public Task DeleteAsync(int id);


    }
}
