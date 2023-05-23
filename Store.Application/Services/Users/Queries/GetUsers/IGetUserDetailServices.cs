﻿using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Constant.Enum;
using Store.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Users.Queries.GetUsers
{
    public interface IGetUserDetailServices
    {
       Task<ResultDto<UserDetailDto>> Execute(long userId);
    }
    public class GetUserDetailServices : IGetUserDetailServices
    {
        private readonly IDataBaseContext _context;
        public GetUserDetailServices(IDataBaseContext context)
        {
            _context=context;
        }
        public async Task<ResultDto<UserDetailDto>> Execute(long userId)
        {
            try
            {
                var user = await _context.Users
                    .Include(p => p.Logins)
                     .Include(p => p.UserInRolls)
                     .Include(p => p.Contacts)
                     .Where(p => p.Id==userId)
                     .FirstOrDefaultAsync();
                if (user!=null)
                {
                    return new ResultDto<UserDetailDto>
                    {
                        Data=new UserDetailDto
                        {
                            Id=user.Id,
                            Name=user?.Name,
                            LastName=user?.LastName,
                            Gender=user?.Gender==null ? 0: user.Gender,
                            IsActive=user.IsActive,
                            Username=user?.Logins.FirstOrDefault()?.UserName,
                            Mobile=user?.Contacts.Where(p => p.ContactTypeId==(long)ContactTypeEnum.Mobile).FirstOrDefault()?.Value,
                            Email=user?.Contacts.Where(p => p.ContactTypeId==(long)ContactTypeEnum.Email).FirstOrDefault()?.Value,
                            Address=user?.Contacts.Where(p => p.ContactTypeId==(long)ContactTypeEnum.Address).FirstOrDefault()?.Value,
                            Rolls=user?.UserInRolls.Select(p => p.RollId).ToArray()
                        },
                        IsSuccess=true
                    };
                }
                else { return new ResultDto<UserDetailDto> { IsSuccess=false, Data=null }; };
            }
            catch(Exception ex)
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
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public int? Gender { get; set; }
        public bool IsActive { get; set; }
        public string? Username { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public long[]? Rolls { get; set; }
    }
}
