using Snai3i_BLL.DTO.AcountDto;
using Snai3i_BLL.DTO.WorkerDto;
using Snai3i_DAL.Data;
using Snai3i_DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.Manager.Acountmanager
{
    public interface IAccountManager
    {
        Task<GeneralResponse> login(LoginDTO log);

        Task<GeneralResponse> Register(RegisterDto RegisterDto);

        Task<ApplicationUser?> GetByIdAsync(string id);
        Task<ApplicationUserReadDto?> GetByIdMailAsync(string email);
        Task signout();
    }
}
