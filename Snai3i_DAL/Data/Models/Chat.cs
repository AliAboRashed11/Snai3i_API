using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Models
{
    public class Chat :  Deletebase , Isoftdelete
    {
        public int Id { get; set; }
        public string SenderId { get; set; }

        public ApplicationUser sender { get; set; } = null;

        public string ReceiverId { get; set; }
        public ApplicationUser receiver { get; set; } = null ;

        public DateTime Date { get; set; }
        public string Message { get; set; }

    }
}
