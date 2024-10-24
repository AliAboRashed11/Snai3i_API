using Microsoft.AspNetCore.Identity;
using Snai3i_BLL.DTO.AdminstrationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.Manager.AdminstrationManage
{
    public interface IAdministrationManager
    {
        Task<IdentityResult> CreateNewRoleAsync(AddRoleDto addRoleDto);

        Task<List<RoleReadDto>> ListRolesAsync();

        Task<EditRoleDto> FindbyIDAsync(string? RoleId);

        Task<IdentityResult> Edit( EditRoleDto editRoleDto);

        //Task<IdentityResult> AssignRoleToUser(string userId, string roleId);
    }

}
