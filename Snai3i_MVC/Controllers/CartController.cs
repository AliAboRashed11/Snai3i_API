using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Snai3i_BLL.DTO.CardDTOS;
using System.Collections.Generic;

namespace YourNamespace.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public async Task<IActionResult> Index(string buyerId)
        {
           

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:7000/");

                var response = await httpClient.GetAsync($"api/cart/{buyerId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var cartItems = JsonConvert.DeserializeObject<List<CardDTO>>(content);
                    return View(cartItems);
                }

                return View(new List<CardDTO>());
            }
        }

        // POST: Cart/Add
        [HttpPost]
        public async Task<IActionResult> Add(CardDTO cardDto)
        {
            if (cardDto == null)
            {
                return BadRequest("Card data is required.");
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:7000/");

                var jsonContent = JsonConvert.SerializeObject(cardDto);
                var response = await httpClient.PostAsync("api/cart/add",
                    new StringContent(jsonContent, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", new { buyerId = cardDto.BuyerId });
                }

                return BadRequest("Unable to add item to cart.");
            }
        }

        // POST: Cart/Increase
        [HttpPost]
        public async Task<IActionResult> Increase(int id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:7000/");

                var response = await httpClient.PostAsync($"api/cart/Increase/{id}", null);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return BadRequest("Unable to increase item count.");
            }
        }

        // POST: Cart/Decrease
        [HttpPost]
        public async Task<IActionResult> Decrease(int id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:7000/");

                var response = await httpClient.PostAsync($"api/cart/Decrease/{id}", null);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return BadRequest("Unable to decrease item count.");
            }
        }

        // POST: Cart/Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:7000/");

                var response = await httpClient.DeleteAsync($"api/cart/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return BadRequest("Unable to delete item from cart.");
            }
        }
    }
}
