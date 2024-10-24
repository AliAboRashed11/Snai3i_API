using Snai3i_DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.DTO.OrderDto
{
    public class OrderAddDTO
    {
        public string UserId { get; set; }           // ID of the user placing the order
        public string WorkerId { get; set; }         // ID of the worker receiving the order
        public double Value { get; set; }           // Total value of the order
        public double Commission { get; set; }      // Commission amount
        public DateTime ForwardDate { get; set; }
         public OrderState State { get; set; }
    }
}
