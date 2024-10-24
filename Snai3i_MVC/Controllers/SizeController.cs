using Microsoft.AspNetCore.Mvc;
using Snai3i_BLL.DTO.SizesDTO;
using Snai3i_DAL.Data.Models;
using Snai3i_MVC.Models;
using System.Diagnostics;
using System.Text.Json;
using System.Text;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Snai3i_BLL.DTO.CraftDto;
using Snai3i_DAL.Data;
using Snai3i_BLL.Manager.ToolsManager;




namespace Snai3i_MVC.Controllers
{
    [Route("Sizes")]
    public class SizeController : Controller

    {

        private readonly HttpClient _httpClient;

        public SizeController(HttpClient httpClient)
        {

            _httpClient = httpClient;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
 

        [HttpGet("Add")]
        public async Task<IActionResult> Add()
        {
          

            return View();

        }

        [HttpPost]
        [Route("SaveData")]
        public async Task<IActionResult> SaveData(AddSizeDTO newSize)
        {
            string url = "https://localhost:7000/api/size";

            // Serialize newCraft to JSON format
            var jsonContent = JsonConvert.SerializeObject(newSize);

            // Create StringContent with JSON data, set content type to application/json
            using (var content = new StringContent(jsonContent, Encoding.UTF8, "application/json"))
            {
                var httpResponse = await _httpClient.PostAsync(url, content);

                if (httpResponse.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAllTools","Tools");
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
