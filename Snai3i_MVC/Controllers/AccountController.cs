using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using Snai3i_BLL.DTO.AcountDto;
using Snai3i_BLL.DTO.WorkerDto;
using Snai3i_DAL.Data.Models;
using Snai3i_MVC.Models;

using System.Diagnostics;
using System.Drawing.Printing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Snai3i_MVC.Controllers
{
    [Route("Accounts")]
    public class AccountController : Controller
    {

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;


        public AccountController(HttpClient httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            this._configuration = configuration;

            _httpClient.BaseAddress = new Uri("https://localhost:7000/");
        }
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View("Index");
        }

        //Account/Login
        [HttpGet("Login")]
        public ActionResult Login()
        {
            return View("Login");
        }


        [HttpPost]
        [Route("LoginSave")]
        public async Task<ActionResult> LoginSave(LoginDTO login)
        {

            // Create the JSON content to send
            var loginData = new
            {
                email = login.email,
                password = login.password
            };

            // Convert the loginData object to JSON format
            var jsonContent = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

            // Send POST request to API
            var httpResponse = await _httpClient.PostAsync("/api/Account/Login", jsonContent);

            if (httpResponse.IsSuccessStatusCode)
            {
                // Read the token from the response body (assuming API sends token in plain text or JSON)
                var responseBody = await httpResponse.Content.ReadAsStringAsync();
                var token = JsonSerializer.Deserialize<GeneralResponse>(responseBody)?.token; // Adjust based on your API response structure

                //Store the token in session
                HttpContext.Session.SetString("AuthToken", token);
                return View( "Logged", login );

                // return RedirectToAction("GetallWorkers", "Worker");
                // Redirect to a relevant page after successful login

                

            }
            else
            {
                // Extract error message from the response and display it
                var error = await httpResponse.Content.ReadAsStringAsync();
                ViewBag.ErrorMessage = "Login failed: " + error;
                return View("Login"  );
            }
        }
        [HttpGet("Profile")]
        public async Task<IActionResult> Profile(string mail)
        {
            var token = HttpContext.Session.GetString("AuthToken");

            var url = $"/api/Account/GetByIdMailAsync?mail={mail}";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", token); 

            var httpresponse = await _httpClient.GetAsync(url);

            if (httpresponse.IsSuccessStatusCode)
            {
                var content = await httpresponse.Content.ReadAsStringAsync();

                var settings = new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                };


                var appuser =  JsonConvert.DeserializeObject<ApplicationUserReadDto>(content
                    , settings);

                return View(appuser);
            }
            return View("Error", new ErrorViewModel { RequestId = 
                Activity.Current?.Id ?? HttpContext.TraceIdentifier });


        }

        [HttpGet("Register")]

        //Account/RegisterUser
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register([FromForm] RegisterDto register)
        {
            if (ModelState.IsValid)
            {
                string url = "/api/Account/Register";

                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StringContent(register.first_name), "first_name");
                    content.Add(new StringContent(register.last_name), "last_name");
                    content.Add(new StringContent(register.email), "email");
                    content.Add(new StringContent(register.password), "password");
                    content.Add(new StringContent(register.ConfirmPassword), "ConfirmPassword");
                    content.Add(new StringContent(register.usertype.ToString()), "usertype");  // Send the user type

                    if (register.imageFile != null)
                    {
                        var fileContent = new StreamContent(register.imageFile.OpenReadStream());
                        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(register.imageFile.ContentType);
                        content.Add(fileContent, "imageFile", register.imageFile.FileName);
                    }

                    var httpResponse = await _httpClient.PostAsync(url, content);

                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var response = await httpResponse.Content.ReadAsStreamAsync();
                        var token = JsonSerializer.Deserialize<GeneralResponse>(response)?.token;

                        HttpContext.Session.SetString("AuthToken", token);

                        return View("Registered");
                    }
                    else
                    {
                        var error = await httpResponse.Content.ReadAsStringAsync();
                        ViewBag.ErrorMessage = "Registration failed: " + error;
                    }
                }
            }

            return View("Register");
        }
        [HttpGet("Signout")]
        public async Task<IActionResult> Signout()
        {
            var token = HttpContext.Session.GetString("AuthToken");


            var url = "/api/Account/SignOut";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", token);

            var httpresponse = await _httpClient.GetAsync(url);



            if (httpresponse.IsSuccessStatusCode) {
                return RedirectToAction("Login" , "Accounts");
            }

            var error = await httpresponse.Content.ReadAsStringAsync();
            ViewBag.ErrorMessage = "Singout failed: " + error;
            return View("Logged"); 
           
        }
        //Accounts/GetLocationData
        //[HttpGet("GetLocationData")]
        //public async Task<IActionResult> GetLocationData(double latitude, double longitude)
        //{
        //    var Id = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "System";
        //    string userurl = "/api/Account/GetByIdAsync/Id";

        //    var response = await _httpClient.GetAsync(userurl);

        //    if (response.IsSuccessStatusCode)
        //    {

        //        var content = await response.Content.ReadAsStringAsync();
        //        var settings = new JsonSerializerSettings
        //        {
        //            MissingMemberHandling = MissingMemberHandling.Ignore,
        //            NullValueHandling = NullValueHandling.Ignore
        //        };
        //        var appuser = JsonConvert.DeserializeObject<ApplicationUser>(content, settings);

        //        string url = $"https://nominatim.openstreetmap.org/reverse?format=jsonv2&lat=" +
        //            $"{latitude}&lon={longitude}&addressdetails=1";

        //        // Send the GET request
        //        var httpResponse = await _httpClient.GetAsync(url);
        //        if (!httpResponse.IsSuccessStatusCode)
        //        {
        //            return BadRequest("Failed to retrieve location data.");
        //        }

        //        // Read the response content
        //        var usercontent = await httpResponse.Content.ReadAsStringAsync();


        //        // Return the JSON content (could also return a custom object if you want to parse it)
        //        return Content(content, "application/json");
        //    }
        //    return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    
    }
}
