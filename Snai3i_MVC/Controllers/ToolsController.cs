using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using Snai3i_BLL.DTO.SizesDTO;
using Snai3i_BLL.DTO.ToolsDTO;
using Snai3i_DAL.Data.Models;
using Snai3i_DAL.Data.Repository.ToolsRepository;
using Snai3i_MVC.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace Snai3i_MVC.Controllers
{
    [Route("Tools")]
    public class ToolsController : Controller
    {
        private readonly IToolRepository _Tools;

        private readonly HttpClient _httpClient;

        public ToolsController( HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        //Tools/GetAllTools
        [HttpGet("GetAllTools")]
        public async Task<IActionResult> GetAllTools()
        {
            string url = "https://localhost:7000/api/Tool/GetTool";

            
            HttpResponseMessage httpResponse = await _httpClient.GetAsync(url);

            
            if (httpResponse.IsSuccessStatusCode)
            {
                
                var responseStream = await httpResponse.Content.ReadAsStreamAsync();

                
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true 
                };

                var tools = await System.Text.Json.JsonSerializer.DeserializeAsync<List<ReadToolDTO>>(responseStream, options);

                return View("GetAllTools",tools);
            }
            else
            {
               
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
        [HttpGet("Getalltool")]

        public async Task<IActionResult> Getalltool(int PageNumber, int pagesize)
        {
            var token = HttpContext.Session.GetString("AuthToken");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Accounts"); // Redirect to login if token is missing
            }

            string url = $"https://localhost:7000/api/Tool/GetAllTools?PageNumber={PageNumber}&pagesize={pagesize}";

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

                var toolsData = await System.Text.Json.JsonSerializer.DeserializeAsync<ToolsDataDto>(responseStream, options);

                return View(toolsData);
            }
            else
            {

                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
        [HttpGet("GetToolById/{id}")]
        public async Task<IActionResult> GetToolById(int id)
        {
            string url = $"https://localhost:7000/api/Tool/{id}";

            HttpResponseMessage httpResponse = await _httpClient.GetAsync(url);

            if (httpResponse.IsSuccessStatusCode)
            {
                var responseStream = await httpResponse.Content.ReadAsStreamAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var tool = await JsonSerializer.DeserializeAsync<ReadToolDTO>(responseStream, options);

               return View("GetToolById", tool);
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
        public async Task<IActionResult> SaveData(AddToolDTO newTool)
        {
            string url = "https://localhost:7000/api/Tool";

            using(var content= new MultipartFormDataContent())
            {
                content.Add(new StringContent(newTool.Name),"Name");
                content.Add(new StringContent(newTool.Description), "Description");

                using(var stream= newTool.imageFile.OpenReadStream())
                {
                    var fileContent= new StreamContent(stream);
                    fileContent.Headers.ContentType=new System.Net.Http.Headers.MediaTypeHeaderValue(newTool.imageFile.ContentType);
                    content.Add(fileContent, "imageFile",newTool.imageFile.FileName);
                    
                    var httpResponse = await _httpClient.PostAsync(url, content);


                    if (httpResponse.IsSuccessStatusCode)
                    {
                       

                        return RedirectToAction("Add","Size");
                    }
                    else
                    {
                        return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                    }
                }
              
            }
            
            //var toolJson = new StringContent(JsonConvert.SerializeObject(newTool), Encoding.UTF8, "application/json");

           
            
        }
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(int id, UpdateToolDTO updatedTool)
        {
            string url = $"https://localhost:7000/api/Tool/{id}";

            
            var toolJson = new StringContent(JsonSerializer.Serialize(updatedTool), Encoding.UTF8, "application/json");

            
            HttpResponseMessage httpResponse = await _httpClient.PutAsync(url, toolJson);

           
            if (httpResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("GetToolById");
            }
            else
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

    }
}
