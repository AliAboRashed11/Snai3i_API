using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.DTO.ToolsDTO
{
    public class UpdateToolDTO
    {

        public int Id {  get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public IFormFile? imageFile { get; set; }


        // public int SizeId { get; set; }



    }
}
