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
        public List<FindedUserDetailByUsernameDto> Excute(string Username);
    }
    public class CheckUserExistByUsernameServices : ICheckUserExistByUsernameServices
    {
        private readonly IDataBaseContext _context;
        public CheckUserExistByUsernameServices(IDataBaseContext context)
        {
            _context = context;
        }
        public List<FindedUserDetailByUsernameDto> Excute(string Username)
        {
            var user = _context.Logins
                .Include(p => p.User)
                .Where(p => p.UserName==Username)
                .Select(p => new FindedUserDetailByUsernameDto()
                {
                    Id=p.User.Id,
                    FullName=p.User.FullName,
                    IsActive=p.User.IsActive
                }).ToList();
            return user;
        }
    }
    public class FindedUserDetailByUsernameDto
    {
        public long Id { get; set; }
        public string? FullName { get; set; }
        public bool IsActive { get; set; }

    }
}
