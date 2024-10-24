using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using Snai3i_BLL.DTO.AcountDto;
using Snai3i_BLL.DTO.WorkerDto;
using Snai3i_BLL.Manager.UserManager;
using Snai3i_DAL.Data;
using Snai3i_DAL.Data.Context;
using Snai3i_DAL.Data.Enums;
using Snai3i_DAL.Data.Models;
using Snai3i_DAL.Data.service.FileService;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_BLL.Manager.Acountmanager
{
    public class AccountManager : IAccountManager
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IConfiguration _configuration;

        private readonly Ifileservice _fileservice;

        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly SnaiiDBContext _context;
        private readonly RoleManager<IdentityRole> rolemanager;
        private readonly IMapper mapper;

        public AccountManager( UserManager<ApplicationUser> userManager , IConfiguration configuration 
            ,SignInManager<ApplicationUser> signInManager , Ifileservice fileservice,
            SnaiiDBContext context
            , RoleManager<IdentityRole> rolemanager,
            
            IMapper mapper )
        { 
            _configuration = configuration;
            _userManager = userManager;

            _fileservice = fileservice;
            _signInManager = signInManager;
            _context = context;
            this.rolemanager = rolemanager;
            this.mapper = mapper;
        }



        public async Task<GeneralResponse> login(LoginDTO log)
        {
            var usermanager =  await _userManager.FindByEmailAsync(log.email);

            if (usermanager == null) {
                throw new Exception(" can't find your email "); 
            }
            var result =  await _userManager.CheckPasswordAsync( usermanager , log.password );
            if (result == false) {
                throw new Exception ( "your password is not correct "); 

            }
            var claims = await _userManager.GetClaimsAsync(usermanager);
            return GenerateToken(claims); 
        }

        public async Task<GeneralResponse> Register(RegisterDto RegisterDto)
        {

            if (RegisterDto.usertype == usertype.User)
            {
                User register = new User()
                {
                    UserName = RegisterDto.first_name,
                    LastName = RegisterDto.last_name,
                    Email = RegisterDto.email
                };
                return await advaneRegisteration(register, RegisterDto);
            }
            else
            {

                Worker register = new Worker()
                {
                    UserName = RegisterDto.first_name,
                    LastName = RegisterDto.last_name,
                    Email = RegisterDto.email
                };
                return await advaneRegisteration(register, RegisterDto);
            }
        }
            public async Task<GeneralResponse>advaneRegisteration(ApplicationUser register,RegisterDto RegisterDto) { 
            // Map usertype to ImageType enum
            ImageType imageType = RegisterDto.usertype == usertype.User ? ImageType.User : ImageType.Worker;

            var imageresult = _fileservice.SaveImage(RegisterDto.imageFile);
            if (imageresult.Item1 == 1)
            {
                register.image = imageresult.Item2;
            }



            IdentityResult result = await _userManager.CreateAsync(register, RegisterDto.password);
            if (!result.Succeeded)
            {
                throw new Exception($"Error: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            string roleId = RegisterDto.usertype == usertype.User ? "role-user-id" : "role-worker-id";

            var role = await rolemanager.FindByIdAsync(roleId);
            if (role != null)
            {

                await _userManager.AddToRoleAsync(register, role.Name);
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, register.Id),
                new Claim("first_name", RegisterDto.first_name),
                new Claim( ClaimTypes.Role , role?.Name ) 
            };

            // Assign role based on usertype


            await _userManager.AddClaimsAsync(register, claims);

            return GenerateToken(claims);
        }


        public async Task<ApplicationUser?> GetByIdAsync(string id)
        {
            return await  _userManager.FindByIdAsync(id);
        }


        public async Task<ApplicationUserReadDto?> GetByIdMailAsync(string email)
        {
            var appuser = await _userManager.FindByEmailAsync(email);
            var appuserrad = new ApplicationUserReadDto
            {
                Email = appuser.Email,
                phone = appuser.PhoneNumber,
                image = appuser.image,
               
                LastName = appuser.LastName,
                FirstName = appuser.UserName,
            };
            return appuserrad;
        }

        public async Task signout()
        {
            _signInManager.SignOutAsync(); 
        }



        private GeneralResponse GenerateToken( IList<Claim> claims)
        {
            var secretKeyString = _configuration.GetValue<string>("SK"); 
            var secretKeyBytes = Encoding.ASCII.GetBytes(secretKeyString);  
            SecurityKey securityKey = new SymmetricSecurityKey ( secretKeyBytes);


            SigningCredentials signingcredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var _expiredate = DateTime.Now.AddDays(1); 
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingcredentials,
                expires: _expiredate
                );
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            string _token = tokenHandler.WriteToken(jwtSecurityToken);   

            return new GeneralResponse()
            {
                token = _token,  
                expiredate = _expiredate
            };  
        }


    }
}
