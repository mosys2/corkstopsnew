using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Pagination;
using Store.Common.ResultDto;
using Store.Domain.Entities.Users;
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
        ResultDto<UsersForAdmin_Dto> Execute(RequestGetUserDto request);
    }
    public class GetUsersServices : IGetUsersServices
    {
        private readonly UserManager<User> _userManager;
        public GetUsersServices(UserManager<User> userManager)
        {
            _userManager=userManager;
        }

        public ResultDto<UsersForAdmin_Dto> Execute(RequestGetUserDto request)
        {
            var users = _userManager.Users.AsQueryable();

            int rowsCount = 0;
            if (!string.IsNullOrWhiteSpace(request.SerachKey))
            {
                users=users.Where(p => p.FullName.Contains(request.SerachKey));
            }
            var userList = users.ToPaged(request.Page, request.PageSize, out rowsCount).OrderByDescending(p => p.InsertTime).Select
                (p => new GetUsersDto
                {
                    FullName=p.FullName,
                    Id=p.Id,
                    LockoutEnabled=p.LockoutEnabled,
                    Email=p.Email,
                    Mobile=p.PhoneNumber
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
        public string Id { get; set; }
        public string? FullName { get; set; }
        public bool LockoutEnabled { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
    }
    

    public class RequestGetUserDto
    {
        public string? SerachKey { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
