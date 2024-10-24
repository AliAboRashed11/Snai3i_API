using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Models
{
    public class Review : Deletebase, Isoftdelete
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User user { get; set; } = null;

        public string WorkerId { get; set; }
        public Worker worker { get; set; } = null;

        public float Rate { get; set; }

        public string Comment { get; set; }

        public int OrderId { get; set; }

        public Order order { get; set; } = null;
    }
}
