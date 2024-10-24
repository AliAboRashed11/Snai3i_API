using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Snai3i_BLL.DTO.AcountDto;
using Snai3i_BLL.Manager.Acountmanager;
using Snai3i_DAL.Data.service.FileService;
using Microsoft.AspNetCore.Http;
using Snai3i_BLL.DTO.WorkerDto;
using Snai3i_BLL.Manager.WorkerManager;
using Snai3i_DAL.Data;

namespace Snai3i_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountManager accountManager;


        public AccountController(IAccountManager accountManager , Ifileservice ifileservice)
        {
            this.accountManager = accountManager;

        }




        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register([FromForm] RegisterDto Register)
        {
            try
            {
                var result = await accountManager.Register(Register);
                return Ok(result);

            }
            catch( Exception ex) { 
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        [Route("Login")]

        public async Task<ActionResult> Login(LoginDTO log)
        {
            try
            {
                var result = await accountManager.login(log);
                return Ok(result);

            }
            catch (Exception ex) { 
                return Unauthorized(ex.Message);
            }

        }


        [HttpGet("SignOut")]
        [Authorize] // Requires the user to be authenticated
        public async Task<IActionResult> SignOut()
        {
            await accountManager.signout();
            return Ok(new { message = "User signed out successfully." });
        }


        [HttpGet("GetByIdAsync")]

        public async Task<IActionResult> GetByIdAsync(string id)
        {
            return Ok(await accountManager.GetByIdAsync(id));
        }


        [HttpGet("GetByIdMailAsync")]

        public async Task<IActionResult> GetByIdMailAsync(string mail)
        {
            return Ok(await accountManager.GetByIdMailAsync(mail));
        }

    }


}