using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Snai3i_DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.DTO.ReviewDto
{
    public class ReviewAddDTO
    {
        public string UserId { get; set; }

        public string WorkerId { get; set; }
        public int OrderId { get; set; }
        public float Rating { get; set; }

        public string Comment { get; set; }


    }
}
