using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Models
{
    public class Card : Deletebase , Isoftdelete
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public int ToolId { get; set; }

        public Tool tool { get; set; }
        public int SizeId { get; set; }

        public Size size { get; set; }
  
        public string BuyerId { get; set; }

        public ApplicationUser buyer { get; set; } = null;

    }
}
