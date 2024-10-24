using Snai3i_BLL.DTO.OrderDto;
using Snai3i_BLL.DTO.ReviewDto;
using Snai3i_BLL.DTO.CraftDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.Manager.OrdersManager
{
    public interface IOrderManager
    {
        public Task AddOrderAsync(OrderAddDTO orderAddDTO);
        Task<IEnumerable<OrderReadDTO>> GetOrdersForWorkerAsync(string workerId);
        //Task<IEnumerable<OrderReadDTO>> GetOrdersForUserAsync(string userId);
        //public Task<IEnumerable<OrderReadDTO>> GetAllOrdersAsync();

        //public Task<OrderReadDTO> GetOrderByIdAsync(int id);

        //public Task EidetOrderAsync(OrderUpdateDTO orderUpdateDTO);

        public Task DeleteOrderAsync(int id);
    }
}
