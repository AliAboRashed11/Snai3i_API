using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Models
{
    public class Tool : Deletebase, Isoftdelete
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public ICollection<ApplicationUser> buyers { get; set; } = new HashSet<ApplicationUser>();



        public ICollection<Size> sizes { get; set; } = new HashSet<Size>();

        public ICollection<Card> cards { get; set; } = new HashSet<Card>();
    }
}
