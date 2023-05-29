using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.ResultDto;
using Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Users.Queries.GetAllRolls
{
    public interface IGetAllRolls_ForAdmin
    {
        Task<ResultDto<List<AllRolls_ForAdminDto>>>  Execute();
    }
    public class GetAllRolls_ForAdmin : IGetAllRolls_ForAdmin
    {
        private readonly RoleManager<Role> _identityRole;
        public GetAllRolls_ForAdmin(RoleManager<Role> identityRole) {
            _identityRole=identityRole;
        }

        public async Task<ResultDto<List<AllRolls_ForAdminDto>>> Execute()
        {
            var rolls = await _identityRole.Roles.Select(p => new AllRolls_ForAdminDto
            {
                Id=p.Id,
                Name=p.Name,
                Description=p.Description
            }).ToListAsync();
            return new ResultDto<List<AllRolls_ForAdminDto>>()
            {
                Data= rolls,
                IsSuccess=true
            };
        }
    }
    public class AllRolls_ForAdminDto
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
