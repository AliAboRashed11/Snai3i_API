using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.DTO.ReviewDto
{
    public class ReviewUpdateDTO
    {
        public int Id { get; set; }
        public float Rate { get; set; }

        public string Comment { get; set; }
    }
}
