using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Snai3i_DAL.Data.Models;
using Snai3i_DAL.Data.Repository.GenericRepository.BasketRepository;

namespace Snai3i_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basket;

        public BasketController(IBasketRepository basket)
        {
            _basket = basket;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await _basket.GetBasketAsync(id);
            return basket ?? new CustomerBasket(id);
        }

        [HttpPost]

        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            var createandupdatebasket = await _basket.UpdateBasketAsync(basket);
            if (createandupdatebasket == null) return BadRequest("/////");
            return createandupdatebasket;
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string Id)
        {
            return await _basket.DeleteBasketAsync(Id);
        }
    }
}
