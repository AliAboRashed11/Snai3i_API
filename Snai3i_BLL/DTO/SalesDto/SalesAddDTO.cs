using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.DTO.SalesDto
{
    public class SalesAddDTO
    {
        public DateTime Date { get; set; }
        public string DeliveryState { get; set; }
        public string Address { get; set; }
        public string OrderState { get; set; }
        public int CardId { get; set; }
        public string UserId { get; set; }     // ID of the User making the purchase
        public string WorkerId { get; set; }
    }
}
