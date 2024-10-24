using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.DTO.CraftDto
{
    public class CraftUdateDTO
    {
        public int CraftId { get; set; }
        public string CraftName { get; set; }

        public string? modifiedbyId { get; set; }

        public IFormFile? imageFile { get; set; }



        public string? modifiedbyName { get; set; }

       

    }
}
