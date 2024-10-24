using Microsoft.AspNetCore.Http;
using Snai3i_BLL.DTO.SalesDto;
using Snai3i_DAL.Data.Models;
using Snai3i_DAL.Data.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.Manager.SalessManager
{
    public class SalesManager : ISalesManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SalesManager(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddSalesAsync(SalesAddDTO salesAddDTO)
        {
            var sale = new Sales
            {
                Date = salesAddDTO.Date,
                DeliveryState = salesAddDTO.DeliveryState,
                Address = salesAddDTO.Address,
                OrderState = salesAddDTO.OrderState,
                CardId = salesAddDTO.CardId,
                UserId = salesAddDTO.UserId,
                WorkerId = salesAddDTO.WorkerId
            };

            _unitOfWork.CreateTransaction();
            await _unitOfWork.Saless.InsertAsync(sale);
            await _unitOfWork.CompleteAsync();
            _unitOfWork.Commit();
        }

        public async Task DeleteAsync(int id)
        {
            var SalesModel = await _unitOfWork.Saless.GetByIdAsync(id);
            if (SalesModel == null)
            {
                throw new Exception(" review dosen't exist ");
            }
            
                SalesModel.deletedbyId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                SalesModel.deleteddatetime = DateTime.Now;
                _unitOfWork.CreateTransaction();
                await _unitOfWork.Saless.DeleteAsync(id);
                await _unitOfWork.CompleteAsync();
                _unitOfWork.Commit();
            
        }

        public async Task<IEnumerable<SalesReadDTO>> GetAllSalesAsync()
        {
            var sales = await _unitOfWork.Saless.GetSalesAsync();
            return sales.Select(s => new SalesReadDTO
            {
                Id = s.Id,
                Date = s.Date,
                DeliveryState = s.DeliveryState,
                Address = s.Address,
                OrderState = s.OrderState,
                UserName = s.user?.UserName,
                WorkerName = s.worker?.UserName
            }).ToList();
        }

        public async Task<SalesReadDTO> GetByIdAsync(int id)
        {
            var sale = await _unitOfWork.Saless.GetSaleByIdAsync(id);
            if (sale == null)
            {
                throw new Exception("not found");
            }

            var SalesReadDTo = new SalesReadDTO
            {
                Id = sale.Id,
                Date = sale.Date,
                DeliveryState = sale.DeliveryState,
                Address = sale.Address,
                OrderState = sale.OrderState,
                UserName = sale.user?.UserName,
                WorkerName = sale.worker?.UserName
            };

            return SalesReadDTo;
        }

        public async Task UpdateAsync(SalesUpdateDTO salesUpdateDTO)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;

            var salesModel = await _unitOfWork.Saless.GetByIdAsync(salesUpdateDTO.Id);
            salesModel.Address = salesUpdateDTO.Address;
            salesModel.DeliveryState =salesUpdateDTO.DeliveryState;
            salesModel.OrderState =salesUpdateDTO.OrderState;

            _unitOfWork.CreateTransaction();

            await _unitOfWork.Saless.UpdateAsync(salesModel);
            await _unitOfWork.CompleteAsync();
            _unitOfWork.Commit();
        }
    }
}
