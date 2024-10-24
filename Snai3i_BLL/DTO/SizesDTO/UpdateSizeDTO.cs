using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.DTO.SizesDTO
{
    public class UpdateSizeDTO
    {

        public int Id { get; set; }
        public double Price { get; set; }

        public double ToolSize { get; set; }

        public int Stock { get; set; }

        public int ToolId { get; set; }



    }
}
