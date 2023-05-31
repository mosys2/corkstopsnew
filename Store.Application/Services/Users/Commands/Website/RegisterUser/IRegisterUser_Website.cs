using Store.Application.Interfaces.Contexts;
using Store.Common.Constant.Enum;
using Store.Common.Constant;
using Store.Common.ResultDto;
using Store.Domain.Entities.Contacts;
using Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Application.Services.Users.Commands.RegisterUser;
using Store.Common.Security;
using Microsoft.AspNetCore.Identity;

namespace Store.Application.Services.Users.Commands.Website.RegisterUser
{
    public interface IRegisterUser_Website
    {
        Task<ResultDto> Execute(RequestRegisterUserDto_Website request);
    }
    public class RegisterUser_Website : IRegisterUser_Website
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public RegisterUser_Website(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        
        public async Task<ResultDto> Execute(RequestRegisterUserDto_Website request)
        {
            try
            {
                User user = new User()
                {
                    FullName=request.FullName,
                    Email=request.Email,
                    UserName=request.Email,
                    PhoneNumber=request.Mobile,
                    InsertTime=DateTime.Now,
                    LockoutEnabled=true,
                    IsRemoved=false
                };
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RollsName.Customer);
                    await _signInManager.PasswordSignInAsync(user.Email, request.Password
                        , false, false);
                    return new ResultDto
                    {
                        IsSuccess=true,
                        Message=Messages.Message.RegisterSuccess
                    };
                }
                else
                {
                    return new ResultDto
                    {
                        IsSuccess=false,
                        Message=Messages.ErrorsMessage.RegisterFeaild
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultDto
                {
                    IsSuccess=false,
                    Message=ex.Message
                };
            }
        }
    }
    public class RequestRegisterUserDto_Website
    {
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
