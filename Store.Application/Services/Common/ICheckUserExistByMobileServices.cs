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
    public interface ICheckUserExistByMobileServices
    {
        Task<List<FindedUserDetailByMobileDto>> Excute(string Mobile, string Id);
    }
    public class CheckUserExistByMobileServices : ICheckUserExistByMobileServices
    {
        private readonly UserManager<User> _context;
        public CheckUserExistByMobileServices(UserManager<User> context)
        {
            _context = context;
        }

        public Task<List<FindedUserDetailByMobileDto>> Excute(string Mobile, string Id)
        {
            throw new NotImplementedException();
        }


        //public async Task<List<FindedUserDetailByMobileDto>> Excute(string Mobile, string Id)
        //{

        //    var user =await _context.Users
        //        .Where(p => p.PhoneNumber==Mobile)
        //        .ToListAsync();
        //    if (String.IsNullOrEmpty(Id))
        //    {
        //        var userList1 = user.Select(p => new FindedUserDetailByMobileDto()
        //        {
        //            Id=p.Id,
        //            FullName=p.FullName,
        //            IsActive=p.LockoutEnabled
        //        }).ToList();
        //        return userList1;
        //    }
        //    else
        //    {
        //        var userList1 = user.Where(p => p.Id!=Id).Select(p => new FindedUserDetailByMobileDto()
        //        {
        //            Id=p.Id,
        //            FullName=p.FullName,
        //            IsActive=p.LockoutEnabled
        //        }).ToList();
        //        return userList1;
        //    }

        //}
    }
    public class FindedUserDetailByMobileDto
    {
        public string Id { get; set; }
        public string? FullName { get; set; }
        public bool IsActive { get; set; }

    }
}
