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

namespace Store.Application.Services.Products.Queries.GetAllTags
{
    public interface IGetAllTagsServices
    {
        Task<List<AllTagsDto>> Execute();
    }
    public class GetAllTagsServices : IGetAllTagsServices
    {
        private readonly IDataBaseContext _context;
        public GetAllTagsServices(IDataBaseContext context)
        {
            _context=context;
        }
        public async Task<List<AllTagsDto>> Execute()
        {
            var tags = await _context.Tags.Select(p => new AllTagsDto
            {
                Id=p.Id,
                Name=p.Name,
            }).ToListAsync();
            return tags;
        }
    }
    public class AllTagsDto
    {
        public string Id { get; set; }
        public string? Name { get; set; }
    }
}
