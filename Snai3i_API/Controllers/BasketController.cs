using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Snai3i_BLL.DTO;
using Snai3i_DAL.Data.Models;
using Snai3i_DAL.Data.Repository.BasketRepositry;

namespace Snai3i_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basket;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basket ,IMapper mapper )
        {
            _basket = basket;
            _mapper = mapper;
        }
        [HttpGet("GetBasket/{id}")]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await _basket.GetBasketAsync(id);
            return basket ?? new CustomerBasket(id);
        }

        [HttpPost]

        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var maapeBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            var createandupdatebasket = await _basket.UpdateBasketAsync(maapeBasket);
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
