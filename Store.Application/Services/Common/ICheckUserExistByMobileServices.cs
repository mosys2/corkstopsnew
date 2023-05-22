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
    public interface ICheckUserExistByMobileServices
    {
        Task<List<FindedUserDetailByMobileDto>> Excute(string Mobile, long Id);
    }
    public class CheckUserExistByMobileServices : ICheckUserExistByMobileServices
    {
        private readonly IDataBaseContext _context;
        public CheckUserExistByMobileServices(IDataBaseContext context)
        {
            _context = context;
        }
        public async Task<List<FindedUserDetailByMobileDto>> Excute(string Mobile, long Id)
        {
            
            var user =await _context.Contacts
                .Include(p => p.User)
                .Where(p => p.Value==Mobile && p.ContactTypeId==(long)ContactTypeEnum.Mobile)
                .ToListAsync();
            if (Id==0)
            {
                var userList1 = user.Select(p => new FindedUserDetailByMobileDto()
                {
                    Id=p.User.Id,
                    FullName=p.User.FullName,
                    IsActive=p.User.IsActive
                }).ToList();
                return userList1;

            }
            else
            {
                var userList1 = user.Where(p => p.User.Id!=Id).Select(p => new FindedUserDetailByMobileDto()
                {
                    Id=p.User.Id,
                    FullName=p.User.FullName,
                    IsActive=p.User.IsActive
                }).ToList();
                return userList1;
            }
            
        }
    }
    public class FindedUserDetailByMobileDto
    {
        public long Id { get; set; }
        public string? FullName { get; set; }
        public bool IsActive { get; set; }

    }
}
