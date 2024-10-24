using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Models
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public CustomerBasket(string id)
        {
            Id = id;
        }
       public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
    }
}
