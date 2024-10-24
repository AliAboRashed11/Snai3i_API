using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Snai3i_BLL.DTO.OrderDto;
using Snai3i_BLL.DTO.SalesDto;
using Snai3i_BLL.Manager.SalessManager;

namespace Snai3i_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISalesManager _salesManager;

        public SalesController(ISalesManager salesManager)
        {
            _salesManager = salesManager;
        }

        // POST: api/sales
        [HttpPost]
        public async Task<IActionResult> CreateSale(SalesAddDTO salesDto)
        {
            await _salesManager.AddSalesAsync(salesDto);
            return Ok("Sale created successfully.");
        }

        // GET: api/sales
        [HttpGet]
        public async Task<IActionResult> GetSales()
        {
            var sales = await _salesManager.GetAllSalesAsync();
            return Ok(sales);
        }

        // GET: api/sales/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSaleById(int id)
        {
            var sale = await _salesManager.GetByIdAsync(id);
            if (sale == null) return NotFound();
            return Ok(sale);
        }
        [HttpPut]
        [Route("{Id}")]
        public async Task<IActionResult> Edit(int Id, SalesUpdateDTO salesUpdateDTO)
        {
            
                if (Id != salesUpdateDTO.Id)
                {
                    return BadRequest();
                }

                await _salesManager.UpdateAsync(salesUpdateDTO);
                return NoContent();  

        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteSales(int Id)
        {
            
                await _salesManager.DeleteAsync(Id);
                return Content("your order is deleted successfuly!");
        }
    }
}
