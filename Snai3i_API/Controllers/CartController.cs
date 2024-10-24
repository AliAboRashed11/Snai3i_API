using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Snai3i_BLL.DTO.CardDTOS;
using Snai3i_BLL.DTO.CraftDto;
using Snai3i_BLL.Manager.CardManager;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Snai3i_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICardManager _cardManager;

        public CartController(ICardManager cardManager)
        {
            _cardManager = cardManager;
        }

		// Get cart by user id
		[HttpGet("{buyerId}")]
		public async Task<IActionResult> GetAll(string buyerId)
		{
			//var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var cards = await _cardManager.GetAllCardsAsync(buyerId);
			return Ok(cards);
		}



		[HttpPost("add")]
		public async Task<IActionResult> Add(CardDTO cardDto)
		{
			//var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			cardDto.BuyerId = cardDto.BuyerId;
			await _cardManager.AddCardAsync(cardDto);
			return Ok();
		}

		// Delete item from cart
		[HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _cardManager.Delete(id);
            return NoContent();
         }

        // Increase item count in cart
        [HttpPost("Increase/{id}")]
        public async Task<IActionResult> Increase(int id)
        {
            await _cardManager.Increase(id);
            return Ok();
        }

        // Decrease item count in cart
        [HttpPost("Decrease/{id}")]
        public async Task<IActionResult> Decrease(int id)
        {
            await _cardManager.Decrease(id);
            return Ok();
        }

		



	}
}
