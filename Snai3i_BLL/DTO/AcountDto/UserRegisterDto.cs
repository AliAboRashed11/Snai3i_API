using Microsoft.AspNetCore.Http;
using Snai3i_DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.DTO.AcountDto
{
    public class UserRegisterDto
    {
        public string first_name { get; set; }
        public string last_name { get; set; }

        public IFormFile? imageFile { get; set; }

        public usertype usertype { get; set; } 

        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [DataType(DataType.Password)]
        public string password { get; set; }

        [DataType(DataType.Password)]

        [Compare("password")]
        public string ConfirmPassword { get; set; }
    }
}
