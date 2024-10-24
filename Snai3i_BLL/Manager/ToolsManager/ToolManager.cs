using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Snai3i_BLL.DTO.AcountDto;
using Snai3i_BLL.DTO.ToolsDTO;
using Snai3i_BLL.DTO.WorkerDto;
using Snai3i_DAL.Data;
using Snai3i_DAL.Data.Models;
using Snai3i_DAL.Data.Repository.ToolsRepository;
using Snai3i_DAL.Data.Repository.UnitOfWork;
using Snai3i_DAL.Data.Repository.WorkerRepo;
using Snai3i_DAL.Data.service.FileService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Snai3i_BLL.Manager.ToolsManager
{
    public class ToolManager : IToolManager
    {
        // D J
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _IMapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Ifileservice fieservice;

        public ToolManager(IUnitOfWork unitOfWork, IMapper mapper ,IHttpContextAccessor httpContextAccessor, Ifileservice fieservice)
        {
            _unitOfWork = unitOfWork;
            _IMapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            this.fieservice = fieservice;
        }


        //Add Tool 
        public async Task AddAsync([FromForm]AddToolDTO toolAddDTO)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var imageresult = fieservice.SaveImage(toolAddDTO.imageFile);
            var ToolModel = new Tool()
            {
                Name = toolAddDTO.Name,
                Image = imageresult.Item2,
                Description = toolAddDTO.Description,
                createdbyId = userId,
                createddatetime = DateTime.Now,
                createdbyName = userName

            };

            await _unitOfWork.Toolss.InsertAsync(ToolModel);
            //await _unitOfWork.Toolss.InsertAsync(_IMapper.Map<Tool>(toolAddDTO));
            await _unitOfWork.CompleteAsync();
        }


        //Delete Tool 
        //public async Task DeleteAsync(int id)
        //{
        //    var ToolModel = await _unitOfWork.Toolss.GetByIdAsync(id);
        //    ToolModel.deletedbyId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    ToolModel.deleteddatetime = DateTime.Now;

        //    await _unitOfWork.Toolss.DeleteAsync(id);
        //    await _unitOfWork.CompleteAsync();
        //}



        public async Task DeleteAsync(int id)
        {

            var ReviewModel = await _unitOfWork.Toolss.GetByIdAsync(id);
            if (ReviewModel == null)
            {
                throw new Exception(" review dosen't exist ");
            }
            try
            {
                ReviewModel.deletedbyId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                ReviewModel.deleteddatetime = DateTime.Now;
                _unitOfWork.CreateTransaction();
                await _unitOfWork.Toolss.DeleteAsync(id);
                await _unitOfWork.CompleteAsync();
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
            }
        }

        public async Task<IEnumerable<ReadToolDTO>> GetSomeToolsAsync()
        {

            return _IMapper.Map<List<ReadToolDTO>>(await _unitOfWork.Toolss.GetSomeToolsAsync());
        }

        //Get All Tool 

        public async Task<ToolsDataDto> GetAllTools(ToolParamters toolParamters)
        {
            var toolpagedlist = await _unitOfWork.Toolss.GetAllTools(toolParamters);

            var tooldata = new ToolsDataDto();

            tooldata.toolsReadList =

                  _IMapper.Map<List<ReadToolDTO>>(toolpagedlist);

            tooldata.data = new ToolsMetaDataDto()
            {
                CurrentPage = toolpagedlist.CurrentPage,
                TotalCount = toolpagedlist.TotalCount,
                HasNextPage = toolpagedlist.HasNextPage,
                HasPreviousPage = toolpagedlist.HasPreviousPage,
                pageSize = toolpagedlist.pageSize,
                TotalPages = toolpagedlist.TotalPages,
            };


            return tooldata;

        }

        public async Task<IEnumerable<ReadToolDTO>> GetAllAsync()
        {

            return _IMapper.Map<List<ReadToolDTO>>(await _unitOfWork.Toolss.GetAllincAsync());
        }


        // Get By Id
        public async Task<ReadToolDTO> GetByIdAsync(int id)
        {
            return _IMapper.Map<ReadToolDTO>(await _unitOfWork.Toolss.GetByIdincAsync(id));
        }


        //Update Tool 
        public async Task UpdateAsync([FromForm]UpdateToolDTO updateToolDTO)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var imageresult = fieservice.SaveImage(updateToolDTO.imageFile);

            var ToolModel = await _unitOfWork.Toolss.GetByIdAsync(updateToolDTO.Id);
            ToolModel.Name = updateToolDTO.Name;
            ToolModel.Image = imageresult.Item2;
            ToolModel.Description = updateToolDTO.Description;
            ToolModel.modifiedDatetime = DateTime.Now;
            ToolModel.modifiedbyName = userName;
            ToolModel.modifiedbyId = userId;
            //_IMapper.Map<ToolUpdateDTO, Tool>(toolUpdateDTO, await _unitOfWork.Toolss.GetByIdAsync(toolUpdateDTO.Id)) 
            await _unitOfWork.Toolss.UpdateAsync(ToolModel);
            await _unitOfWork.CompleteAsync();
        }

       

    }
}
