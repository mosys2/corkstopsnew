using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Pagination;
using Store.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Users.Queries.GetUsers
{
    public interface IGetUsersServices
    {
       public ResultDto<UsersForAdmin_Dto> Execute(RequestGetUserDto request);
    }
    public class GetUsersServices : IGetUsersServices
    {
        private readonly IDataBaseContext _context;
        public GetUsersServices(IDataBaseContext context)
        {
            _context=context;
        }

        public ResultDto<UsersForAdmin_Dto> Execute(RequestGetUserDto request)
        {
            var users = _context.Users.Include(p=>p.Contacts).ThenInclude(c=>c.ContactType).AsQueryable();
            int rowsCount = 0;
            if (!string.IsNullOrWhiteSpace(request.SerachKey))
            {
                users=users.Where(p=>p.FullName.Contains(request.SerachKey));
            }
            var userList = users.ToPaged(request.Page, request.PageSize, out rowsCount).OrderByDescending(p => p.InsertTime).Select
                (p => new GetUsersDto
                {
                        FullName=p.FullName,
                        Id=p.Id,
                        IsActive=p.IsActive,
                        ContactsUser=p.Contacts.Select(c=>new ContactsUsersDto
                        {
                            ContactValue=c.Value,
                            Icon=c.ContactType.Icon
                        }).ToList()
                   
                }).ToList();
            return new ResultDto<UsersForAdmin_Dto>()
            {
                Data=new UsersForAdmin_Dto()
                {
                    CurentPage=request.Page,
                    PageSize=request.PageSize,
                    RowCount=rowsCount,
                    Users= userList
                }
            };

        }
    }

    public class UsersForAdmin_Dto
    {
        public int RowCount { get; set; }
        public int CurentPage { get; set; }
        public int PageSize { get; set; }
        public List<GetUsersDto> Users { get; set; }
        
    }
    public class GetUsersDto
    {
        public long Id { get; set; }
        public string? FullName { get; set; }
        public bool IsActive { get; set; }
        public List<ContactsUsersDto> ContactsUser { get; set; }
    }
    public class ContactsUsersDto
    {
        public string? Icon { get; set; }
        public string? ContactValue { get; set; }
    }

    public class RequestGetUserDto
    {
        public string? SerachKey { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
