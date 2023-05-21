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
        public List<FindedUserDetailByMobileDto> Excute(string Email);
    }
    public class CheckUserExistByMobileServices : ICheckUserExistByMobileServices
    {
        private readonly IDataBaseContext _context;
        public CheckUserExistByMobileServices(IDataBaseContext context)
        {
            _context = context;
        }
        public List<FindedUserDetailByMobileDto> Excute(string Email)
        {
            var user = _context.Contacts
                .Include(p => p.User)
                .Where(p => p.Value==Email && p.ContactTypeId==(long)ContactTypeEnum.Mobile)
                .Select(p => new FindedUserDetailByMobileDto()
                {
                    Id=p.User.Id,
                    FullName=p.User.FullName,
                    IsActive=p.User.IsActive
                }).ToList();
            return user;
        }
    }
    public class FindedUserDetailByMobileDto
    {
        public long Id { get; set; }
        public string? FullName { get; set; }
        public bool IsActive { get; set; }

    }
}
