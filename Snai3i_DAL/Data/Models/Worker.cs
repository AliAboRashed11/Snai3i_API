using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Models
{
    public class Worker : ApplicationUser, Isoftdelete
    { 
        public int? Nationalnumber { get; set; }
        public string? Governorate {  get; set; }

        public DateTime? StartingDate { get; set; } 

        public int? NumberOfOperation {  get; set; }

        public int? CraftId { get; set; }

        public Craft? craft { get; set; } 
        public  ICollection<User> users { get; set; }  = new HashSet<User>();

        public ICollection<Review> reviews { get; set; } = new HashSet<Review>();

        public ICollection<Order> orders { get; set; } = new HashSet<Order>();

    }
}
