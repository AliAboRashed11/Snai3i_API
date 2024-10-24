using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.DTO.AdminstrationDto
{
    public class AddRoleDto
    {
        [Required]
        [Display(Name ="Role")]
        public string RoleName { get; set; }
    }
}
