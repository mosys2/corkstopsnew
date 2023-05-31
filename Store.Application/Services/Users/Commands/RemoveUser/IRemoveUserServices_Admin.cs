using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Constant;
using Store.Common.ResultDto;
using Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Users.Commands.RemoveUser
{
    public interface IRemoveUserServices_Admin
    {
        Task<ResultDto> Execute(string Id);
    }
    public class RemoveUserServices_Admin : IRemoveUserServices_Admin
    {
        private readonly UserManager<User> _userManager;

        public RemoveUserServices_Admin(UserManager<User> userManager)
        {
            _userManager=userManager;
        }

        public async Task<ResultDto> Execute(string Id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(Id);
                if (user == null) { return new ResultDto() { IsSuccess=false, Message="User not found!" }; }

                user.IsRemoved=true;
                user.RemoveTime=DateTime.Now;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return new ResultDto
                    {
                        IsSuccess = true,
                        Message =Messages.Message.RemovedSuccess
                    };
                }
                else
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message =Messages.ErrorsMessage.WrongRemove
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message =ex.Message
                };
            }

        }


    }
}
