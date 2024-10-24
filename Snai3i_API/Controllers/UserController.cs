using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Snai3i_BLL.DTO.WorkerDto;
using Snai3i_BLL.Manager.UserManager;
using Snai3i_BLL.Manager.WorkerManager;
using Snai3i_DAL.Data;

namespace Snai3i_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IuserManager usermanage;

        public UserController(IuserManager usermanage)
        {
            this.usermanage = usermanage;
        }

        [HttpPut]
        [Route("userId")]

        public async Task<ActionResult> Setlocation(string location, string userId)
        {
            try
            {

                return Ok(await usermanage.setlocation(location, userId)); 
            }
            catch (Exception ex) {
                return NotFound(ex.Message); 
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetAllworkers([FromQuery] UserParameters userparameters)
        {
            var userdata = await usermanage.GetAllUsers(userparameters);

            return Ok(userdata);
        }

    }
}
