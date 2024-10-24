using Microsoft.AspNetCore.Mvc;
using Snai3i_BLL.DTO.ReviewDto;
using Snai3i_MVC.Models;
using System.Diagnostics;
using System.Text.Json;
using System.Text;
using Snai3i_DAL.Data.Models;
using NuGet.Versioning;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace Snai3i_MVC.Controllers
{
    [Route("Review")]

    public class ReviewController : Controller
    {
        private readonly HttpClient _httpClient;

        public ReviewController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [HttpGet("GetAllReviews")]
        public async Task<IActionResult> GetAllReviews()
        {
            string url = "https://localhost:7000/api/Reviews";


            HttpResponseMessage httpResponse = await _httpClient.GetAsync(url);


            if (httpResponse.IsSuccessStatusCode)
            {

                var responseStream = await httpResponse.Content.ReadAsStreamAsync();


                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var Reviews = await System.Text.Json.JsonSerializer.DeserializeAsync<List<ReviewReadDTO>>(responseStream, options);

                return View("GetAllReviews", Reviews);
            }
            else
            {

                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
        [HttpGet("GetReviewById/{id}")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            string url = $"https://localhost:7000/api/Reviews/{id}";

            HttpResponseMessage httpResponse = await _httpClient.GetAsync(url);

            if (httpResponse.IsSuccessStatusCode)
            {
                var responseStream = await httpResponse.Content.ReadAsStreamAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var Review = await JsonSerializer.DeserializeAsync<ReviewReadDTO>(responseStream, options);

                return View("GetReviewById", Review);
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
        public async Task<IActionResult> SaveData(ReviewAddDTO newReview)
        {
            string url = "https://localhost:7000/api/Reviews";

            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StringContent(newReview.UserId), "UserId");
                content.Add(new StringContent(newReview.WorkerId), "WorkerId");

                content.Add(new StringContent(newReview.Comment), "Comment");


                
                    // Serialize the float value to JSON
                    var ratingContent = JsonConvert.SerializeObject(new { Rating = newReview.Rating });

                    // Add the JSON as StringContent and specify application/json as media type
                    var stringContent = new StringContent(ratingContent);
                    stringContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    content.Add(stringContent);

              

                var httpResponse = await _httpClient.PostAsync(url, content);


                    if (httpResponse.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetAllReviews");
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
        public async Task<IActionResult> Edit(int id, ReviewUpdateDTO updatedReview)
        {
            string url = $"https://localhost:7000/api/Reviews/{id}";


            var toolJson = new StringContent(JsonSerializer.Serialize(updatedReview), Encoding.UTF8, "application/json");


            HttpResponseMessage httpResponse = await _httpClient.PutAsync(url, toolJson);


            if (httpResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("GetReviewById/{id}");
            }
            else
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }
}
