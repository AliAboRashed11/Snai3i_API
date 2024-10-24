using Microsoft.AspNetCore.Mvc;
using Snai3i_BLL.DTO.CraftDto;
using Snai3i_BLL.DTO.ToolsDTO;
using Snai3i_BLL.DTO.WorkerDto;
using Snai3i_MVC.Models;
using Snai3i_MVC.ViewModels;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;

namespace Snai3i_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            string toolsUrl = "https://localhost:7000/api/Tool/GetTool";
            string craftsUrl = "https://localhost:7000/api/Crafts";

            var toolsTask = _httpClient.GetAsync(toolsUrl);
            var craftsTask = _httpClient.GetAsync(craftsUrl);
            //var workersTask = _httpClient.GetAsync(workersUrl);

            await Task.WhenAll(toolsTask, craftsTask );

            var toolsResponse = toolsTask.Result;
            var craftsResponse = craftsTask.Result;
            //var workersResponse = workersTask.Result;

            if (toolsResponse.IsSuccessStatusCode && craftsResponse.IsSuccessStatusCode )
            {
                var toolsContent = await toolsResponse.Content.ReadAsStringAsync();
                var craftsContent = await craftsResponse.Content.ReadAsStringAsync();
                //var workersContent = await workersResponse.Content.ReadAsStringAsync();


                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true 
                    };

                var tools = JsonSerializer.Deserialize<List<ReadToolDTO>>(toolsContent, options);
                var crafts = JsonSerializer.Deserialize<List<CraftReadDTO>>(craftsContent, options);

                //var workers = JsonSerializer.Deserialize<List<WorkerReadDto>>(workersContent, options);

                var viewModel = new ModelForIndex
                    {
                        Tools = tools,
                        Crafts = crafts
                        //Workers = Workers
                    };

                    return View(viewModel);
                }
                else
                {
                    
                    return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                }
            
        }
        public IActionResult About()
        {
            ViewData["Message"] = "This platform connects artisans with clients and offers tool sales.";
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}