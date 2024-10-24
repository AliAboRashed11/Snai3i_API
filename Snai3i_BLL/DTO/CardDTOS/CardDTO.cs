using Snai3i_DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.DTO.CardDTOS
{
	public class CardDTO
	{
        public int Id { get; set; }
        public int ToolId { get; set; }
		public int SizeId { get; set; }
        
        public int Quantity { get; set; }
		public string BuyerId { get; set; }

		// Additional properties for display purposes
		public string ToolName { get; set; }
		public double Size { get; set; }

		public double Price { get; set; }
		public double TotalPrice { get; set; }
	}
}
