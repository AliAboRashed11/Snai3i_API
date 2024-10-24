using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.DTO.SalesDto
{
    public class SalesUpdateDTO
    {
        public int Id { get; set; }
        public string OrderState { get; set; }
        public string DeliveryState { get; set; }
        public string Address { get; set; }
    }
}
