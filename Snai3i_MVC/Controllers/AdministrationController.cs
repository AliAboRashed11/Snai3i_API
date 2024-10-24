using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Snai3i_BLL.DTO.AdminstrationDto;
using System.Net.Http.Headers;
using System.Security.Permissions;
using System.Text;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace Snai3i_MVC.Controllers
{
    [Route("Administration")]
    public class AdministrationController : Controller
    {
        private readonly HttpClient httpClient;

        public AdministrationController(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            httpClient.BaseAddress = new Uri("https://localhost:7000/");
        }


        //here's seeding of our roles 
        // Seeding roles
        //var adminRole = new IdentityRole { Id = "role-admin-id", Name = "Admin", NormalizedName = "ADMIN" };
        //var superAdminRole = new IdentityRole { Id = "role-superadmin-id", Name = "SuperAdmin", NormalizedName = "SUPERADMIN" };
        //var userRole = new IdentityRole { Id = "role-user-id", Name = "User", NormalizedName = "USER" };
        //var workerRole = new IdentityRole { Id = "role-worker-id", Name = "Worker", NormalizedName = "WORKER" };

        //Administration/CreateRole
        [HttpGet("CreateRoles")]
        public async Task<IActionResult> CreateRoles()
        {
            var token = HttpContext.Session.GetString("AuthToken");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Accounts");
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync("/api/Administration/CreateRoleGet");
            if (response.IsSuccessStatusCode)
            {
                return View(new AddRoleDto() );
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                ViewBag.ErrorMessage = "Login failed: " + error;
                return RedirectToAction("ListRoles", "Administration");
            }
        }

        [HttpPost("CreateRoles")]
        public async Task<IActionResult> CreateRoles(AddRoleDto addRoleDto)
        {
            if (ModelState.IsValid)
            {

                var token = HttpContext.Session.GetString("AuthToken");

                var JsonContent = new StringContent(JsonSerializer.Serialize(addRoleDto),
                    Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.PostAsync("/api/Administration/CreateRole"
                    , JsonContent);


                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("ListRoles", "Administration");

                }
                var error = await response.Content.ReadAsStringAsync();
                ViewBag.ErrorMessage = "Login failed: " + error;
                return RedirectToAction("ListRoles", "Administration");
            }

            return View(addRoleDto);


        }

        //Administration/ListRoles
        [HttpGet("ListRoles")]
        public async Task<IActionResult> ListRoles()
        {
            var token = HttpContext.Session.GetString("AuthToken");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Accounts");
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.GetAsync("/api/Administration/ListRoles");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();// error is here 
                // this is just strig we need it as list of RolesReadDto

                var settings = new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                };

                var Roles = JsonConvert.DeserializeObject<List<RoleReadDto>>(content , settings);
                return View(Roles);
            }
            var error = await response.Content.ReadAsStringAsync();
            ViewBag.ErrorMessage = "Login failed: " + error;
            return RedirectToAction("Login", "Accounts");
        }

        // Edit Role 
        [HttpGet("EditRole")]
        public async Task<IActionResult> EditRole( string RoleId)
        {


                var token = HttpContext.Session.GetString("AuthToken");

                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("Login", "Accounts");
                }
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token); 

                var Response =await httpClient.GetAsync($"/api/Administration/EditRoles?RoleId={RoleId}"); 

                if(Response.IsSuccessStatusCode)
                {
                    var content = await Response.Content.ReadAsStringAsync();

                    var settings = new JsonSerializerSettings
                    {
                        MissingMemberHandling = MissingMemberHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore
                    }; 

                    var EditRole = JsonConvert.DeserializeObject<EditRoleDto>(content, settings);
                    return View(EditRole);
                }
                var error = await Response.Content.ReadAsStringAsync();
                ViewBag.ErrorMessage = "Login failed: " + error;
                return RedirectToAction("ListRoles", "Administration");


        }


        [HttpPost("EditRole")]

        public async Task<IActionResult> EditRole( EditRoleDto Editrole)
        {
            if (ModelState.IsValid)
            {
                var token = HttpContext.Session.GetString("AuthToken");

                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token );

                var content = new StringContent ( JsonConvert.SerializeObject(Editrole) 
                    , Encoding.UTF8 , "application/json");

                var response = await httpClient.PutAsync("api/Administration/PostEditRole"
                    , content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("ListRoles", "Administration");

                }
                var error = await response.Content.ReadAsStringAsync();
                ViewBag.ErrorMessage = "Login failed: " + error;
                return RedirectToAction("ListRoles", "Administration");


            }
            return View("EditRole", Editrole);
        }

    }
}