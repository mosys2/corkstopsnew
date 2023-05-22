using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.ResultDto;
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
        private readonly IDataBaseContext _context;
        public GetAllRolls_ForAdmin(IDataBaseContext context) {
            _context=context;
        }
        public async Task<ResultDto<List<AllRolls_ForAdminDto>>> Execute()
        {
            var rolls =await _context.Rolls.Select(p => new AllRolls_ForAdminDto
            {
                Id=p.Id,
                RollName=p.RollName,
                Title=p.Title
            }).ToListAsync();
            return new ResultDto<List<AllRolls_ForAdminDto>>()
            {
                
                Data= rolls.Select(p=>new AllRolls_ForAdminDto
                {
                    Id=p.Id,
                    RollName=p.RollName,
                    Title=p.Title
                }).ToList(),
                IsSuccess=true
            };
        }
    }
    public class AllRolls_ForAdminDto
    {
        public long Id { get; set; }
        public string? RollName { get; set; }
        public string? Title { get; set; }
    }
}
