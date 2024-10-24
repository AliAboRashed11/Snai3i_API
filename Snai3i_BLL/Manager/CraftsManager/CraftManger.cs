using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Snai3i_BLL.DTO.CraftDto;
using Snai3i_BLL.DTO.ToolsDTO;
using Snai3i_DAL.Data.Models;
using Snai3i_DAL.Data.Repository.CraftsRepository;
using Snai3i_DAL.Data.service;
using Snai3i_DAL.Data.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Snai3i_DAL.Data.service.FileService;
using Microsoft.AspNetCore.Http;

namespace Snai3i_BLL.Manager.CraftsManager
{
    public class CraftManger : ICraftManger
    {
        private readonly ICraftRepo _craftRepo;
        private readonly Ifileservice _ifileservice;
        private readonly IMapper _IMapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CraftManger(ICraftRepo craftRepo, Ifileservice ifileservice)
        {
            _craftRepo = craftRepo;
            _ifileservice = ifileservice;
        }

        //Add Craft 
        public async Task AddCraftAsync([FromForm] CraftAddDTO craft)
        {
            var imageresult = _ifileservice.SaveImage(craft.imageFile);
            var craftModel = new Craft()
            {

                CraftName = craft.CraftName,
                createdbyId = craft.createdbyId,
                createdbyName = craft.createdbyName,
                createddatetime = DateTime.Now,
                PictureUrl = imageresult.Item2

            };
            await _craftRepo.InsertAsync(craftModel);
            await _craftRepo.saveAsync();
        }

        //Delet Craft
        public async Task DeleteCraftAsync(int id)
        {
            var CraftModel = _craftRepo.GetCraftByIdAsync(id);
            _craftRepo.Delete(CraftModel);
            await _craftRepo.saveAsync();

        }

        //Eidet Craft 
        public async Task EidetCraftAsync([FromForm] CraftUdateDTO craft)
        {
            var imageresult = _ifileservice.SaveImage(craft.imageFile);
            var CraftModel = await _craftRepo.GetByIdAsync(craft.CraftId);
            CraftModel.CraftName = craft.CraftName;
            CraftModel.modifiedbyId = craft.modifiedbyId;
            CraftModel.modifiedbyName = craft.modifiedbyName;
            CraftModel.modifiedDatetime = DateTime.Now;
            CraftModel.PictureUrl = imageresult.Item2;
            await _craftRepo.UpdateAsync(CraftModel);
            await _craftRepo.saveAsync();

        }

        //Get all Craft 
        public async Task<IEnumerable<CraftReadDTO>> GetAllCraftAsync()
        {
            var CraftModel = await _craftRepo.GetAllAsync();
            var CafteDto = CraftModel.Select(x => new CraftReadDTO() {Id = x.CraftId, CraftName = x.CraftName, PictureUrl = x.PictureUrl });

            return CafteDto;
            //return _IMapper.Map<CraftReadDTO>(await _craftRepo.GetAllAsync());

        }

        //get by Id 
        public async Task<CraftReadDTO> GetCraftByIdAsync(int id)
        {
            var CraftModel = await _craftRepo.GetByIdAsync(id);

            var CraftReadDTo = new CraftReadDTO()
            {
                CraftName = CraftModel.CraftName
            };
            return CraftReadDTo;
        }
    }
}
