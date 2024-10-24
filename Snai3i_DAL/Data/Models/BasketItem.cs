using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Models
{
  public class BasketItem
    {
		public string Name { get; set; }

		public int Quantity { get; set; }

        public int ToolId { get; set; }

		public decimal Price { get; set; }
		public string Tool { get; set; }

		public string Size { get; set; }

	}
}

