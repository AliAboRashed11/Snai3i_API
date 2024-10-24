using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data.Models
{
    public enum OrderState
    {
        Confirmed,
        notConfirmedYet,
        Rejected,
    }
    public class Order : Deletebase, Isoftdelete
    {
        public int Id { get; set; }

        public DateTime ForwardDate { get; set; }

        public DateTime ConfirmDate { get; set; }

        public string UserId { get; set; }
        public User user { get; set; } = null;

        public string WorkerId { get; set; }
        public Worker worker { get; set; } = null;
        public double Value { get; set; }

        public double Commision { get; set; }

        public int reviewId { get; set; }

        public Review? review { get; set; }

        public  OrderState State { get; set; }


    }
}
