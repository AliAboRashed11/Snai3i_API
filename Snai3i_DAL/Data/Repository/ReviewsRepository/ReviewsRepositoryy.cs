using Microsoft.EntityFrameworkCore;
using Snai3i_DAL.Data.Context;
using Snai3i_DAL.Data.Models;
using Snai3i_DAL.Data.Repository.GenericRepository;
using Snai3i_DAL.Data.Repository.SalesRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Repository.ReviewsRepository
{
    public class ReviewsRepositoryy : GenericRepository<Review>, IReviewsRepository
    {
       
        //private readonly SnaiiDBContext _context;
        public ReviewsRepositoryy(SnaiiDBContext context) : base(context)
        {
            //_context = context;
        }

        public async Task<IEnumerable<Review>> GetReviewsForWorkerAsync(string WorkerId)
        {
            return await   Context.reviews.Where(r => r.WorkerId == WorkerId).
                Include(w => w.user ).Include( w => w.worker).ToListAsync();
        }

        public async Task<Review> getreviewsbyid(int id)
        {
            return await Context.reviews.Include( r => r.user).Include( r => r.worker )
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
