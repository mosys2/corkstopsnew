using Microsoft.AspNetCore.Identity;
using Store.Application.Interfaces.Contexts;
using Store.Common.Constant;
using Store.Common.Constant.Enum;
using Store.Common.ResultDto;
using Store.Domain.Entities.Contacts;
using Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Users.Commands.RegisterUser
{
    public interface IRegisterUser_Admin
    {
        Task<ResultDto> Execute(RequestRegisterUserDto request);
    }
    public class RegisterUser_Admin : IRegisterUser_Admin
    {
        private readonly UserManager<User> _userManager;
        public RegisterUser_Admin(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ResultDto> Execute(RequestRegisterUserDto request)
        {
            User user = new User()
            {
                Name=request.Name,
                LastName=request.LastName,
                FullName=request.Name+" "+request.LastName,
                Gender=request.Gender,
                Email=request.Email,
                UserName=request.Email,
                PhoneNumber=request.Mobile,
                InsertTime=DateTime.Now,
                LockoutEnabled=request.LockoutEnabled,
                IsRemoved=false
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, request.Role);
                return new ResultDto
                {
                    IsSuccess=true,
                    Message=Messages.Message.RegisterSuccess
                };
            }
            return new ResultDto
            {
                IsSuccess=false,
                Message=result.Errors.ToString()
            };

        }
    }

    public class RequestRegisterUserDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public bool LockoutEnabled { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<string> Role { get; set; }
    }
}
