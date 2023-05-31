using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Store.Application.Interfaces.Contexts;
using Store.Application.Services.Users.Commands.Website.RegisterUser;
using Store.Common.Constant;
using Store.Common.Constant.Enum;
using Store.Common.ResultDto;
using Store.Common.Security;
using Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mst_Cms.Application.Services.Users.Command.LoginUser
{
    public interface ILoginUserService
    {
        Task<ResultDto<LoginData>> Execute(string EmailOrMobile, string Password, bool IsPersistent);
    }
    public class LoginUserService : ILoginUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public LoginUserService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager=signInManager;
        }
        public async Task<ResultDto<LoginData>> Execute(string EmailOrMobile, string Password, bool IsPersistent)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(EmailOrMobile,
                          Password, IsPersistent, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(EmailOrMobile);
                    var rollsResult = await _userManager.GetRolesAsync(user);
                    List<string> roles = new List<string>();
                    foreach (var item in rollsResult)
                    {
                        roles.Add(item);
                    }
                    return new ResultDto<LoginData>
                    {
                        Data=new LoginData()
                        {
                            roles=roles,
                        },
                        IsSuccess=true,
                        Message=Messages.Message.LoginSuccess
                    };
                }
                else
                {
                    return new ResultDto<LoginData>
                    {
                        IsSuccess=false,
                        Message=Messages.ErrorsMessage.WrongEmailOrPass
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultDto<LoginData>
                {
                    IsSuccess=false,
                    Message=ex.Message
                };
            }
        }

    }
    public class LoginData
    {
        public List<string> roles { get; set; }
    }
}

