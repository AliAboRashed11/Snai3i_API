using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snai3i_BLL.DTO.ToolsDTO;
using Snai3i_BLL.DTO.WorkerDto;
using Snai3i_MVC.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Http;


using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Snai3i_MVC.Controllers
{
    [Route("Worker")]
    public class WorkerController : Controller
    {
        private readonly HttpClient _httpClient;

        public WorkerController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7000/");
        }
        public IActionResult Index()
        {
            return View();
        }

        //Worker/GetallWorkers?PageNumber=1&pagesize=1
        [HttpGet("GetWorkerByCraft")]

        public async Task<IActionResult> GetWorkerByCraft(int id)
        {
            string url = $"https://localhost:7000/api/Worker/{id}";


            HttpResponseMessage httpResponse = await _httpClient.GetAsync(url);


            if (httpResponse.IsSuccessStatusCode)
            {

                var responseStream = await httpResponse.Content.ReadAsStreamAsync();


                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var worker = await System.Text.Json.JsonSerializer.DeserializeAsync<List<WorkerReadDto>>(responseStream, options);

                return View(worker);
            }
            else
            {

                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
        [HttpGet("GetallWorkers")]

        public async Task<IActionResult> GetallWorkers(int PageNumber , int pagesize )
        {
            var token = HttpContext.Session.GetString("AuthToken");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Accounts"); // Redirect to login if token is missing
            }

            string url = $"api/Worker?PageNumber={PageNumber}&pagesize={pagesize}";

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

                var workerdata = await System.Text.Json.JsonSerializer.DeserializeAsync<WorkerDataDto>(responseStream, options);

                return View(workerdata);
            }
            else
            {

                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }
}
