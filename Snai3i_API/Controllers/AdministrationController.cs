using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Snai3i_BLL.DTO.AdminstrationDto;
using Snai3i_BLL.Manager.AdminstrationManage;

namespace Snai3i_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
        private readonly IAdministrationManager administrationmanager;

        public AdministrationController(IAdministrationManager administrationmanager)
        {
            this.administrationmanager = administrationmanager;
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("CreateRoleGet")]
        public async Task<IActionResult> CreateRoleGet()
        {
            return Ok();
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("CreateRole")]
        public async Task<ActionResult> CreateRole(AddRoleDto roledto)
        {
            var result = await administrationmanager.CreateNewRoleAsync(roledto);

            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpGet("ListRoles")]

        public async Task<ActionResult> ListRoles() { 
            return Ok(await administrationmanager.ListRolesAsync() );
        }



        [Authorize( Roles = "SuperAdmin")]
        [HttpGet("EditRoles")]

        public async Task<IActionResult> EditRoles(string RoleId)
        {
            return Ok(await administrationmanager.FindbyIDAsync(RoleId)); 
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPut("PostEditRole")]

        public async Task<IActionResult> PostEditRole( EditRoleDto editRoleDto)
        {
            return Ok(await administrationmanager.Edit( editRoleDto));
        }


    }
}
