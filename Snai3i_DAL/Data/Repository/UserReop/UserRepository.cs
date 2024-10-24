using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Snai3i_BLL.DTO.WorkerDto;
using Snai3i_DAL.Data.Context;
using Snai3i_DAL.Data.Models;
using Snai3i_DAL.Data.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Repository.UserReop
{
    public class UserRepository : GenericRepository<User>, IuserRepository
    {
        public UserRepository(SnaiiDBContext context) : base(context) { }


        [Authorize]
        public async Task<PagedList<ApplicationUser>> GetAllWorkers(UserParameters userparams)
        {
            var userpagedlist = Context.applicationUsers
                                  .Where(a => a.GetType() == typeof(Worker)); 

            return await PagedList<ApplicationUser>.ToPagedList(userpagedlist
                , userparams.pagesize,
                userparams.PageNumber);
        }

    }
}
