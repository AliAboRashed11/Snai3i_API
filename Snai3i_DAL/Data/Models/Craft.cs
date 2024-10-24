using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Models
{
    public class Craft :  Deletebase , Isoftdelete
    {
        public int CraftId { get; set; }

        public string  CraftName { get; set; }

        public string PictureUrl { get; set; }


        public ICollection<Worker> workers { get; set; } = new HashSet<Worker>();
    }
}
