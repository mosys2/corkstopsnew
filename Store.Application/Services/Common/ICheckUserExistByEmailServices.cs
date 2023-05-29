using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Constant.Enum;
using Store.Common.ResultDto;
using Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Common
{
    public interface ICheckUserExistByEmailServices
    {
        Task<List<FindedUserDetailByEmailDto>> Excute(string Email, string Id);
    }
    public class CheckUserExistByEmailServices : ICheckUserExistByEmailServices
    {
        private readonly UserManager<User> _context;
        public CheckUserExistByEmailServices(UserManager<User> context)
        {
            _context = context;
        }

        public Task<List<FindedUserDetailByEmailDto>> Excute(string Email, string Id)
        {
            throw new NotImplementedException();
        }


        //public async Task<List<FindedUserDetailByEmailDto>> Excute(string Email, string Id)
        //{
        //    var user = await _context.Users
        //        .Where(p => p.Email==Email)
        //        .ToListAsync();
        //    if (String.IsNullOrEmpty(Id))
        //    {
        //        var userList1 = user.Select(p => new FindedUserDetailByEmailDto()
        //        {
        //            Id=p.Id,
        //            FullName=p.FullName,
        //            IsActive=p.LockoutEnabled
        //        }).ToList();
        //        return userList1;
        //    }
        //    else
        //    {
        //        var userList1 = user.Where(p => p.Id!=Id).Select(p => new FindedUserDetailByEmailDto()
        //        {
        //            Id=p.Id,
        //            FullName=p.FullName,
        //            IsActive=p.LockoutEnabled
        //        }).ToList();
        //        return userList1;
        //    }

        //}
    }
    public class FindedUserDetailByEmailDto
    {
        public string Id { get; set; }
        public string? FullName { get; set; }
        public bool IsActive { get; set; }

    }
}
