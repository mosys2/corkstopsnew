using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Constant.Enum;
using Store.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Common
{
    public interface ICheckUserExistByUsernameServices
    {
        Task<List<FindedUserDetailByUsernameDto>> Excute(string Username, long Id);
    }
    public class CheckUserExistByUsernameServices : ICheckUserExistByUsernameServices
    {
        private readonly IDataBaseContext _context;
        public CheckUserExistByUsernameServices(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<List<FindedUserDetailByUsernameDto>> Excute(string Username, long Id)
        {
            throw new NotImplementedException();
        }
        //public async Task<List<FindedUserDetailByUsernameDto>> Excute(string Username,long Id)
        //{
        //    var user =await _context.Logins
        //        .Include(p => p.User)
        //        .Where(p => p.UserName==Username)
        //        .ToListAsync();
        //    if (Id==0)
        //    {
        //        var userList1 = user.Select(p => new FindedUserDetailByUsernameDto()
        //        {
        //            Id=p.User.Id,
        //            FullName=p.User.FullName,
        //            IsActive=p.User.IsActive
        //        }).ToList();
        //        return userList1;
        //    }
        //    else
        //    {
        //        var userList1 = user.Where(p => p.User.Id!=Id).Select(p => new FindedUserDetailByUsernameDto()
        //        {
        //            Id=p.User.Id,
        //            FullName=p.User.FullName,
        //            IsActive=p.User.IsActive
        //        }).ToList();
        //        return userList1;
        //    }
        //}
    }
    public class FindedUserDetailByUsernameDto
    {
        public long Id { get; set; }
        public string? FullName { get; set; }
        public bool IsActive { get; set; }

    }
}
