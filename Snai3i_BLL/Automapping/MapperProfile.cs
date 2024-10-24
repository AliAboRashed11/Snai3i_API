using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Snai3i_BLL.DTO.AcountDto;
using Snai3i_BLL.DTO.AdminstrationDto;
using Snai3i_BLL.DTO.CardDTOS;
using Snai3i_BLL.DTO.CraftDto;
using Snai3i_BLL.DTO.ToolsDTO;
using Snai3i_BLL.DTO.UserDto;
using Snai3i_BLL.DTO.WorkerDto;
using Snai3i_BLL.DTO.CardDTOS;

using Snai3i_BLL.DTO;

using Snai3i_DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.Automapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Tool, ReadToolDTO>()
            .ForMember(dest => dest.sizes, opt => opt.MapFrom(src => src.sizes));


            CreateMap<Tool, ReadToolDTO>().ReverseMap();
            CreateMap<Tool, AddToolDTO>().ReverseMap();
            CreateMap<Tool, UpdateToolDTO>().ReverseMap();
            //CreateMap<Tool, DeleteToolDTO>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();

            //Cearte Card 
            CreateMap<Card, CardDTO>().ReverseMap();
            CreateMap<Size, ReadSizeDTO>().ReverseMap();

            CreateMap<Craft, CraftAddDTO>().ReverseMap();
            CreateMap<Craft, CraftReadDTO>().ReverseMap();
            CreateMap<Craft, CraftUdateDTO>().ReverseMap();

            CreateMap<ApplicationUser, WorkerReadDto>()
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.UserName)).
                ForMember(dest => dest.phone, opt => opt.MapFrom(src => src.PhoneNumber))
                .ReverseMap();

            CreateMap<ApplicationUser, UserReadDto>()
            .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.UserName)).
            ForMember(dest => dest.phone, opt => opt.MapFrom(src => src.PhoneNumber))
            .ReverseMap();

            CreateMap<IdentityRole, RoleReadDto>().
                ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Id)).
                ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name)).
                ReverseMap();
            CreateMap<IdentityRole, EditRoleDto>().
                ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)).
                ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();
        }

    }
}
