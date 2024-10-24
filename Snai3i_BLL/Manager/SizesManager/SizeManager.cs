using Microsoft.AspNetCore.Http;
using Snai3i_BLL.DTO.SizesDTO;
using Snai3i_BLL.DTO.ToolsDTO;
using Snai3i_DAL.Data.Models;
using Snai3i_DAL.Data.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.Manager.SizesManager
{
    public class SizeManager : ISizeManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SizeManager(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        //Add Sizze 
        public async Task AddAsync(AddSizeDTO addSizeDTO)
        {
            //var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
        
            var SizeModel = new Size()
            {
                ToolSize = addSizeDTO.ToolSize,
                Price = addSizeDTO.Price,
                Stock = addSizeDTO.Stock,
                ToolId = addSizeDTO.ToolId,
                createdbyId = "1",
                createddatetime = DateTime.Now,
                createdbyName = "Ahmed"

            };

            await _unitOfWork.Sizee.InsertAsync(SizeModel);
            //await _unitOfWork.Toolss.InsertAsync(_IMapper.Map<Tool>(toolAddDTO));
            await _unitOfWork.CompleteAsync();
        }


        //delete Size 
        public async Task DeleteAsync(int id)
        {

            var ReviewModel = await _unitOfWork.Sizee.GetByIdAsync(id);
            if (ReviewModel == null)
            {
                throw new Exception(" review dosen't exist ");
            }
            try
            {
               // ReviewModel.deletedbyId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                ReviewModel.deleteddatetime = DateTime.Now;
                _unitOfWork.CreateTransaction();
                await _unitOfWork.Sizee.DeleteAsync(id);
                await _unitOfWork.CompleteAsync();
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
            }
        }



        public async Task UpdateAsync(UpdateSizeDTO updateSizeDTO)
        {
            //var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //var userName = _httpContextAccessor.HttpContext.User.Identity.Name;

            var SizeModel = await _unitOfWork.Sizee.GetByIdAsync(updateSizeDTO.Id);
           SizeModel.Stock = updateSizeDTO.Stock;
           SizeModel.Price = updateSizeDTO.Price;
           SizeModel.ToolSize = updateSizeDTO.ToolSize;
           SizeModel.ToolId = updateSizeDTO.ToolId;
           SizeModel.modifiedDatetime = DateTime.Now;
           SizeModel.modifiedbyName = "userName";
           SizeModel.modifiedbyId = "userId";
            //_IMapper.Map<ToolUpdateDTO, Tool>(toolUpdateDTO, await _unitOfWork.Toolss.GetByIdAsync(toolUpdateDTO.Id)) 
            await _unitOfWork.Sizee.UpdateAsync(SizeModel);
            await _unitOfWork.CompleteAsync();
        }
    }
}
