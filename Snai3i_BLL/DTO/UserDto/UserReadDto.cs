using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.DTO.UserDto
{
    public class UserReadDto
    {
        public string LastName { get; set; }

        public string Firstname { get; set; }

        public string email { get; set; }

        public long phone { get; set; }

        public double? Longitude { get; set; }
        public double? Latitude { get; set; }

        public string RoleName { get; set; }
        public string image { get; set; }

        public bool Isdeleted { get; set; }
        public string? deletedbyId { get; set; }

        public DateTime? deleteddatetime { get; set; }

        public string? createdbyId { get; set; }

        public string? createdbyName { get; set; }

        public DateTime? createddatetime { get; set; }

        public string? modifiedbyId { get; set; }

        public string? modifiedbyName { get; set; }

        public DateTime? modifiedDatetime { get; set; }
    }
}
