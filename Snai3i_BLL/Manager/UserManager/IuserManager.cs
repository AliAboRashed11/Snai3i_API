
using Snai3i_BLL.DTO.UserDto;
using Snai3i_BLL.DTO.WorkerDto;
using Snai3i_DAL.Data;
using Snai3i_DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.Manager.UserManager
{
    public interface IuserManager
    {

        Task<User> setlocation(string location , string userId);

        Task<UserDataDto> GetAllUsers(UserParameters UserParameters);
    }
}
