using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.DTO.SalesDto
{
    public class SalesReadDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string DeliveryState { get; set; }
        public string Address { get; set; }
        public string OrderState { get; set; }
        /*public string CardDetails { get; set; }*/  // Retrieved from Card entity
        public string UserName { get; set; }     // Retrieved from User entity
        public string WorkerName { get; set; }
    }
}
