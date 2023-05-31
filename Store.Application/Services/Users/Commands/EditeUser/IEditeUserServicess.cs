using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

namespace Store.Application.Services.Users.Commands.EditeUser
{
    public interface IEditeUserServicess
    {
        Task<ResultDto> Execute(UserEditeDetailDto request);
    }
    public class EditeUserServicess : IEditeUserServicess
    {
        private readonly UserManager<User> _userManager;
        public EditeUserServicess(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ResultDto> Execute(UserEditeDetailDto request)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.Id);
                if (user==null) { return new ResultDto() { IsSuccess=false, Message="User not found!" }; }

                user.Name=request.Name;
                user.LastName=request.LastName;
                user.FullName=request.Name + " "+request.LastName;
                user.Email=request.Email;
                user.UserName=request.Email;
                user.PhoneNumber=request.Mobile;
                user.Gender=request.Gender;
                user.LockoutEnabled=request.LockoutEnabled;
                user.UpdateTime=DateTime.Now;

                //Update Rols 
                var rols= await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user,rols);
                await _userManager.AddToRolesAsync(user, request.Rols);

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return new ResultDto
                    {
                        IsSuccess=true,
                        Message=Messages.Message.UpdateSuccess
                    };
                }
                else
                {
                    return new ResultDto
                    {
                        IsSuccess=false,
                        Message=Messages.ErrorsMessage.UpdateField
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
    public class UserEditeDetailDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public List<string> Rols { get; set; }
        public int Gender { get; set; }
        public bool LockoutEnabled { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
    }
}
