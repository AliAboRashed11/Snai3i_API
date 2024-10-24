using Microsoft.EntityFrameworkCore;
using Snai3i_DAL.Data.Context;
using Snai3i_DAL.Data.Models;
using Snai3i_DAL.Data.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Repository.OrdersRepository
{
    public class OrdersRepositoryy : GenericRepository<Order>, IOrdersRepository
    {
        //private readonly SnaiiDBContext _context;
        public OrdersRepositoryy(SnaiiDBContext context) : base(context)
        {

        }
        //public async Task AddOrderAsync(Order order)
        //{
        //    await Context.orders.AddAsync(order);

        //}

        public async Task<IEnumerable<Order>> GetOrdersForWorkerAsync(string workerId)
        {
            return await Context.orders
                                 .Where(o => o.WorkerId == workerId)
                                 .Include(o => o.user)
                                 .Include(o => o.worker)
                                 .ToListAsync();
        }
        //public async Task<IEnumerable<Order>> GetOrdersForUserAsync(string userId)
        //{
        //    return await Context.orders
        //                         .Where(o => o.UserId==userId)
        //                         .Include(o => o.user)
        //                         .Include(o => o.worker)
        //                         .ToListAsync();
        //}


    }
}
