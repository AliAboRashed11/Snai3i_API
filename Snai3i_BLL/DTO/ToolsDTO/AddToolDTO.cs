using Microsoft.AspNetCore.Http;
using Snai3i_DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.DTO.ToolsDTO
{
    public class AddToolDTO
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public IFormFile? imageFile { get; set; }

         ICollection<ReadSizeDTO> sizes { get; set; } 




    }
}
