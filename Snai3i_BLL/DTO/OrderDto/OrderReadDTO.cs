using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.DTO.OrderDto
{
    public class OrderReadDTO
    {
        public int Id { get; set; }
        public DateTime ForwardDate { get; set; }
        public DateTime? ConfirmDate { get; set; }
        ////public string UserName { get; set; }
        public string WorkerName { get; set; }
        public double Value { get; set; }
        public double Commission { get; set; }
    }
}
