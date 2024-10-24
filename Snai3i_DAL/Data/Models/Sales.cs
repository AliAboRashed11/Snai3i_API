using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Models
{
    public class Sales : Deletebase, Isoftdelete
    {
        public int Id { get; set; }


        public DateTime Date { get; set; }

        public string DeliveryState { get; set; }

        public string Address { get; set; }

        public string OrderState { get; set; }

        public string UserId { get; set; }

        public User user { get; set; } = null;

        public string WorkerId { get; set; }

        public Worker worker { get; set; } = null;

        public int CardId { get; set; }

        public Card card { get; set; } = null;


    }



}
