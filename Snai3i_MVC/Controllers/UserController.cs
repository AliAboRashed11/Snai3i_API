using Microsoft.AspNetCore.Mvc;
using Snai3i_BLL.DTO.WorkerDto;
using Snai3i_MVC.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using Snai3i_BLL.DTO.UserDto;

namespace Snai3i_MVC.Controllers
{
    [Route("Users")]
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;

        public UserController(HttpClient httpclient)
        {
            _httpClient = httpclient;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("GetallUsers")]

        public async Task<IActionResult> GetallUsers(int PageNumber, int pagesize)
        {
            var token = HttpContext.Session.GetString("AuthToken");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Accounts"); // Redirect to login if token is missing
            }

            string url = $"api/User?PageNumber={PageNumber}&pagesize={pagesize}";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //var request = new HttpRequestMessage(HttpMethod.Get, url);
            //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token); // Add the token to Authorization header




            HttpResponseMessage httpResponse = await _httpClient.GetAsync(url);


            if (httpResponse.IsSuccessStatusCode)
            {

                var responseStream = await httpResponse.Content.ReadAsStreamAsync();


                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var workerdata = await System.Text.Json.JsonSerializer.DeserializeAsync<UserReadDto>(responseStream, options);

                return View(workerdata);
            }
            else
            {

                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

    }
}
