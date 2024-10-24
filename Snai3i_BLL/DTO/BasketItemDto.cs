using Snai3i_DAL.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Snai3i_BLL.DTO
{
    public class BasketItemDto
    {
        [Required]
        public string  Name { get; set; }
        [Required]
        [Range(1,int.MaxValue,ErrorMessage = "Quantity must be one item at leats")]
        public int Quantity { get; set; }
        [Required]
        public int ToolId { get; set; }

        [Required]
        
        [Range(0.1,double.MaxValue,ErrorMessage = "Price Must be greater" )]
        public decimal Price { get; set; }



    }
}