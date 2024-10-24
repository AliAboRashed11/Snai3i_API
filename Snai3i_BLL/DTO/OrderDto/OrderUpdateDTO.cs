using Snai3i_DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.DTO.OrderDto
{
    public class OrderUpdateDTO
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public OrderState State { get; set; }
    }
}
