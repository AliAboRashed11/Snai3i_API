using Snai3i_BLL.DTO.WorkerDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.DTO.UserDto
{
    public class UserDataDto
    {
        public MetaDataDto data { get; set; }

        public List<UserReadDto> userReadList { get; set; }

    }
}
