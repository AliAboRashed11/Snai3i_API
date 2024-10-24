using Snai3i_DAL.Data.Models;
using Snai3i_DAL.Data.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Repository.OrdersRepository
{
    public interface IOrdersRepository:IGenericRepository<Order>
    {
        //Task AddOrderAsync(Order order);
        Task<IEnumerable<Order>> GetOrdersForWorkerAsync(string workerId);
        //Task<IEnumerable<Order>> GetOrdersForUserAsync(string userId);
        //Task<Order> GetOrderByIdAsync(int orderId);
        
    }
}
