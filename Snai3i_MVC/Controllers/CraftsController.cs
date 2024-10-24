using Microsoft.AspNetCore.Mvc;
using Snai3i_BLL.DTO.CraftDto;
using Snai3i_DAL.Data.Models;
using Snai3i_MVC.Models;
using System.Diagnostics;
using System.Text.Json;
using System.Text;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace Snai3i_MVC.Controllers
{
    [Route("Crafts")]

    public class CraftsController : Controller
    {
        private readonly HttpClient _httpClient;

        public CraftsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        //Crafts/GetAllCrafts
        [HttpGet("GetAllCrafts")]
        public async Task<IActionResult> GetAllCrafts()
        {
            string url = "https://localhost:7000/api/Crafts";


            HttpResponseMessage httpResponse = await _httpClient.GetAsync(url);


            if (httpResponse.IsSuccessStatusCode)
            {

                var responseStream = await httpResponse.Content.ReadAsStreamAsync();


                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var crafts = await System.Text.Json.JsonSerializer.DeserializeAsync<List<CraftReadDTO>>(responseStream, options);

                return View("GetAllCrafts", crafts);
            }
            else
            {

                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
        [HttpGet("GetCraftById/{id}")]
        public async Task<IActionResult> GetCraftById(int id)
        {
            string url = $"https://localhost:7000/api/Crafts/{id}";

            HttpResponseMessage httpResponse = await _httpClient.GetAsync(url);

            if (httpResponse.IsSuccessStatusCode)
            {
                var responseStream = await httpResponse.Content.ReadAsStreamAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var craft = await JsonSerializer.DeserializeAsync<CraftReadDTO>(responseStream, options);

                return View("GetCraftById", craft);
            }
            else
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        [HttpGet("Add")]
        public async Task<IActionResult> Add()
        {
            return View("Add");

        }

        [HttpPost]
        [Route("SaveData")]
        public async Task<IActionResult> SaveData(CraftAddDTO newCraft)
        {
            string url = "https://localhost:7000/api/Crafts";

            // Serialize newCraft to JSON format
            var jsonContent = JsonConvert.SerializeObject(newCraft);

            // Create StringContent with JSON data, set content type to application/json
            using (var content = new StringContent(jsonContent, Encoding.UTF8, "application/json"))
            {
                var httpResponse = await _httpClient.PostAsync(url, content);

                if (httpResponse.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAllCrafts");
                }
                else
                {
                    return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                }
            }
        }






        [HttpPut]
        [Route("Tools/Edit/{id}")]
        public async Task<IActionResult> Edit(int id, CraftUdateDTO updatedCraft)
        {
            string url = $"https://localhost:7000/api/Crafts/{id}";


            var toolJson = new StringContent(JsonSerializer.Serialize(updatedCraft), Encoding.UTF8, "application/json");


            HttpResponseMessage httpResponse = await _httpClient.PutAsync(url, toolJson);


            if (httpResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

    }
}

