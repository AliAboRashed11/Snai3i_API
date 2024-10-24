using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.DTO.AcountDto
{
    public class ApplicationUserReadDto
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }
        public string image { get; set; }


        public double? Longitude { get; set; }
        public double? Latitude { get; set; }

        public string? Governorate { get; set; }

        public string? Email { get; set; }

        public string phone { get; set; }

    }
}
