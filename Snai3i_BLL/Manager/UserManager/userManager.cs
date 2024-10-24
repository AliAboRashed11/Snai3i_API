
using Snai3i_BLL.DTO.WorkerDto;
using Snai3i_BLL.DTO;
using Snai3i_DAL.Data.Models;
using Snai3i_DAL.Data.Repository.UserReop;
using Snai3i_DAL.Data.Repository.WorkerRepo;
using Snai3i_DAL.Data.service.MapService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snai3i_BLL.DTO.UserDto;
using Snai3i_DAL.Data;
using AutoMapper;

namespace Snai3i_BLL.Manager.UserManager
{
    public class userManager : IuserManager
    {
        private readonly IMapService _mapService;
        private readonly IuserRepository _userrepository;
        private readonly IMapper mapper;

        public userManager( IMapService mapService , IuserRepository userrepository ,
            IMapper mapper )
        {
            _mapService = mapService;
            _userrepository = userrepository;
            this.mapper = mapper;
        }
        public async Task<User> setlocation(string location, string userId)
        {
            var user = await _userrepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            try
            {
                CustomMap customMap = await _mapService.GetLocationFromAddressAsync(location);
                user.Longitude = customMap.longitude;
                user.Latitude = customMap.latitude;

                // Save changes if you haven't already
                await _userrepository.saveAsync(); // Ensure you're saving the user's updated location

                return user;
            }
            catch (HttpRequestException httpEx)
            {
                throw new Exception("Error occurred while calling the map service: " + httpEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred: " + ex.Message);
            }
        }
        public async Task<UserDataDto> GetAllUsers(UserParameters userparameters)
        {
            var userpagedlist = await _userrepository.GetAllWorkers(userparameters);

            var userdata = new UserDataDto();

            userdata.userReadList =
                mapper.Map<List<UserReadDto>>(userpagedlist);

            userdata.data = new MetaDataDto()
            {
                CurrentPage = userpagedlist.CurrentPage,
                TotalCount = userpagedlist.TotalCount,
                HasNextPage = userpagedlist.HasNextPage,
                HasPreviousPage = userpagedlist.HasPreviousPage,
                pageSize = userpagedlist.pageSize,
                TotalPages = userpagedlist.TotalPages,
            };


            return userdata;

        }

    }
}
