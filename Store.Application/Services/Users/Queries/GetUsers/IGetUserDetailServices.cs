using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Constant.Enum;
using Store.Common.Pagination;
using Store.Common.ResultDto;
using Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Users.Queries.GetUsers
{
    public interface IGetUserDetailServices
    {
       Task<ResultDto<UserDetailDto>> Execute(string userId);
    }
    public class GetUserDetailServices : IGetUserDetailServices
    {
        private readonly UserManager<User> _userManager;
        public GetUserDetailServices(UserManager<User> userManager)
        {
            _userManager=userManager;
        }

        public async Task<ResultDto<UserDetailDto>> Execute(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                List<string> rols = new List<string>();
                rols.AddRange(_userManager.GetRolesAsync(user).Result);
                if (user!=null)
                {
                    return new ResultDto<UserDetailDto>
                    {
                        Data=new UserDetailDto
                        {
                            Id=user.Id,
                            Name=user?.Name,
                            LastName=user?.LastName,
                            Gender=user?.Gender==null ? 0 : user.Gender,
                            LockoutEnabled=user.LockoutEnabled,
                            Mobile=user.PhoneNumber,
                            Email=user.Email,
                            Rols=rols
                        },
                        IsSuccess=true
                    };
                }
                else { return new ResultDto<UserDetailDto> { IsSuccess=false, Data=null }; };
            }
            catch (Exception ex)
            {
                return new ResultDto<UserDetailDto>
                {
                    IsSuccess=false,
                    Message=ex.Message
                };
            }

        }
    }
    public class UserDetailDto
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public int? Gender { get; set; }
        public bool LockoutEnabled { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public List<string>? Rols { get; set; }
    }
}
