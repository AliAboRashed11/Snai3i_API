using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.DTO.CraftDto
{
    public class CraftAddDTO
    {



        public string CraftName { get; set; }

        public string? createdbyId { get; set; }

        public string? createdbyName { get; set; }

        public IFormFile? imageFile { get; set; }



    }
}
