using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.DTO.AdminstrationDto
{
    public class EditRoleDto
    {
        [Required]
        public string Id { get; set; }
        [Required(ErrorMessage ="Role name is required ")]
        public string Name { get; set; }
    }
}
