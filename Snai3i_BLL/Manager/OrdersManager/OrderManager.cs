using Microsoft.AspNetCore.Http;
using Snai3i_BLL.DTO.OrderDto;
using Snai3i_BLL.DTO.ReviewDto;
using Snai3i_DAL.Data.Models;
using Snai3i_DAL.Data.Repository.OrdersRepository;
using Snai3i_DAL.Data.Repository.UserWorkerTransactionUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.Manager.OrdersManager
{
    public class OrderManager : IOrderManager
    {
        private readonly IUserWorkerTransactionUnitOfWork _userWorkerTransaction;
        private readonly IHttpContextAccessor _httpContextAccessor;

        //private readonly IOrdersRepository _orderRepository;

        public OrderManager(IUserWorkerTransactionUnitOfWork userWorkerTransaction,
            IHttpContextAccessor httpContextAccessor)
        {
            //_orderRepository = orderRepository;
            _userWorkerTransaction = userWorkerTransaction;
            _httpContextAccessor = httpContextAccessor;
        }

        // Add a new order
        public async Task AddOrderAsync(OrderAddDTO orderAddDTO)
        {
            try
            {
                //var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                //var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
                var order = new Order()
                {
                    
                    UserId = orderAddDTO.UserId,
                    WorkerId = orderAddDTO.WorkerId,
                    Value = orderAddDTO.Value,
                    Commision = orderAddDTO.Commission,
                    ForwardDate = orderAddDTO.ForwardDate,
                    State=orderAddDTO.State
                    
                };

                _userWorkerTransaction.CreateTransaction();

                await _userWorkerTransaction.Orderss.InsertAsync(order);

                await _userWorkerTransaction.Save();
                _userWorkerTransaction.Commit();
            }
            catch (Exception ex)
            {
                _userWorkerTransaction.Rollback();
            }
        }

        public async Task DeleteOrderAsync(int id)
        {
            var OrderModel = await _userWorkerTransaction.Orderss.GetByIdAsync(id);
            if (OrderModel == null)
            {
                throw new Exception(" order dosen't exist ");
            }
            try
            {
                OrderModel.deletedbyId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                OrderModel.deleteddatetime = DateTime.Now;
                _userWorkerTransaction.CreateTransaction();
                await _userWorkerTransaction.Orderss.DeleteAsync(id);
                await _userWorkerTransaction.Save();
                _userWorkerTransaction.Commit();
            }
            catch (Exception ex)
            {
                _userWorkerTransaction.Rollback();
            }
        }

        //public async Task EidetOrderAsync(OrderUpdateDTO orderUpdateDTO)
        //{
        //    try
        //    {


        //        var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //        var userName = _httpContextAccessor.HttpContext.User.Identity.Name;

        //        var OrderModel = await _userWorkerTransaction.Orderss.GetByIdAsync(orderUpdateDTO.Id);
        //        OrderModel.Value = orderUpdateDTO.Value;
        //        OrderModel.State = orderUpdateDTO.State;

        //        _userWorkerTransaction.CreateTransaction();

        //        await _userWorkerTransaction.Orderss.UpdateAsync(OrderModel);
        //        await _userWorkerTransaction.Save();
        //        _userWorkerTransaction.Commit();

        //    }
        //    catch (Exception ex)
        //    {
        //        _userWorkerTransaction.Rollback();
        //    }
        //}

        //public async Task<IEnumerable<OrderReadDTO>> GetAllOrdersAsync()
        //{
        //    var OrderModel = await _userWorkerTransaction.Orderss.GetAllAsync();
        //    var OrderDTO = OrderModel.Select(x => new OrderReadDTO()
        //    {
        //        Id = x.Id,
        //        ForwardDate = x.ForwardDate,
        //        ConfirmDate = x.ConfirmDate,
        //        WorkerName = x.worker.UserName,
        //        Value = x.Value,
        //        Commission = x.Commision,
        //    });

        //    return OrderDTO;
        //}

        //public async Task<OrderReadDTO> GetOrderByIdAsync(int id)
        //{
        //    var OrderModel = await _userWorkerTransaction.Orderss.GetByIdAsync(id);
        //    if (OrderModel == null)
        //    {
        //        throw new Exception("not found");
        //    }

        //    var OrderReadDTo = new OrderReadDTO
        //    {
        //        Id = OrderModel.Id,
        //        ForwardDate = OrderModel.ForwardDate,
        //        ConfirmDate = OrderModel.ConfirmDate,
        //        WorkerName = OrderModel.worker.UserName,
        //        Value = OrderModel.Value,
        //        Commission = OrderModel.Commision,
        //    };



        //    return OrderReadDTo;
        //}

        //public async Task<IEnumerable<OrderReadDTO>> GetOrdersForUserAsync(string userId)
        //{
        //    var orders = await _userWorkerTransaction.Orderss.GetOrdersForUserAsync(userId);
        //    if (orders == null)
        //    {
        //        throw new Exception("order not found");
        //    }
        //    return orders.Select(o => new OrderReadDTO
        //    {
        //        Id = o.Id,
        //        ForwardDate = o.ForwardDate,
        //        ConfirmDate = o.ConfirmDate,
        //        //UserName = o.user.UserName,
        //        WorkerName = o.worker.UserName,
        //        Value = o.Value,
        //        Commission = o.Commision
        //    }).ToList();
        //}

        // Get orders for a specific worker
        public async Task<IEnumerable<OrderReadDTO>> GetOrdersForWorkerAsync(string workerId)
        {
            var orders = await _userWorkerTransaction.Orderss.GetOrdersForWorkerAsync(workerId);
            if (orders == null)
            {
                throw new Exception("order not found");
            }
            return orders.Select(o => new OrderReadDTO
            {
                Id = o.Id,
                ForwardDate = o.ForwardDate,
                ConfirmDate = o.ConfirmDate,
                //UserName = o.user.UserName,
                WorkerName = o.worker.UserName,
                Value = o.Value,
                Commission = o.Commision
            }).ToList();
        }

      
    }
}
