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
    public interface ICheckUserExistByEmailServices
    {
        Task<List<FindedUserDetailByEmailDto>> Excute(string Email, long Id);
    }
    public class CheckUserExistByEmailServices : ICheckUserExistByEmailServices
    {
        private readonly IDataBaseContext _context;
        public CheckUserExistByEmailServices(IDataBaseContext context)
        {
            _context = context;
        }
        public async Task<List<FindedUserDetailByEmailDto>> Excute(string Email, long Id)
        {
            var user =await  _context.Contacts
                .Include(p => p.User)
                .Where(p => p.Value==Email && p.ContactTypeId==(long)ContactTypeEnum.Email)
                .ToListAsync();
            if (Id==0)
            {
                var userList1=user.Select(p => new FindedUserDetailByEmailDto()
                {
                    Id=p.User.Id,
                    FullName=p.User.FullName,
                    IsActive=p.User.IsActive
                }).ToList();
                return userList1;

            }
            else
            {
                var userList1 =user.Where(p=>p.User.Id!=Id).Select(p => new FindedUserDetailByEmailDto()
                {
                    Id=p.User.Id,
                    FullName=p.User.FullName,
                    IsActive=p.User.IsActive
                }).ToList();
                return userList1;
            }
            
        }
    }
    public class FindedUserDetailByEmailDto
    {
        public long Id { get; set; }
        public string? FullName { get; set; }
        public bool IsActive { get; set; }

    }
}
