using Microsoft.AspNetCore.Mvc;
using Snai3i_BLL.DTO.OrderDto;
using Snai3i_MVC.Models;
using System.Diagnostics;
using System.Text.Json;
using System.Text;

namespace Snai3i_MVC.Controllers
{
    [Route("Order")]

    public class OrderController : Controller
    {
        private readonly HttpClient _httpClient;

        public OrderController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [HttpGet("GetAllOrders")]
        //public async Task<IActionResult> GetAllOrders()
        //{
        //    string url = "https://localhost:7000/api/Order";


        //    HttpResponseMessage httpResponse = await _httpClient.GetAsync(url);


        //    if (httpResponse.IsSuccessStatusCode)
        //    {

        //        var responseStream = await httpResponse.Content.ReadAsStreamAsync();


        //        var options = new JsonSerializerOptions
        //        {
        //            PropertyNameCaseInsensitive = true
        //        };

        //        var orders = await System.Text.Json.JsonSerializer.DeserializeAsync<List<OrderReadDTO>>(responseStream, options);

        //        return View("GetAllOrders", orders);
        //    }
        //    else
        //    {

        //        return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //    }
        //}
        [HttpGet("GetOrderById/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            string url = $"https://localhost:7000/api/Order/{id}";

            HttpResponseMessage httpResponse = await _httpClient.GetAsync(url);

            if (httpResponse.IsSuccessStatusCode)
            {
                var responseStream = await httpResponse.Content.ReadAsStreamAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var order = await JsonSerializer.DeserializeAsync<OrderReadDTO>(responseStream, options);

                return View("GetOrderById", order);
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
        public async Task<IActionResult> SaveData(OrderReadDTO newOrder)
        {
            string url = "https://localhost:7000/api/Order";

            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StringContent(newOrder.WorkerName), "WorkerName");

               
                    var httpResponse = await _httpClient.PostAsync(url, content);


                    if (httpResponse.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetAllOrders");
                    }
                    else
                    {
                        return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                    }
                

            }

            //var toolJson = new StringContent(JsonConvert.SerializeObject(newTool), Encoding.UTF8, "application/json");



        }
        [HttpPut]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, OrderUpdateDTO updatedorder)
        {
            string url = $"https://localhost:7000/api/Order/{id}";


            var toolJson = new StringContent(JsonSerializer.Serialize(updatedorder), Encoding.UTF8, "application/json");


            HttpResponseMessage httpResponse = await _httpClient.PutAsync(url, toolJson);


            if (httpResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("GetOrderById/{Id}");
            }
            else
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }
}
