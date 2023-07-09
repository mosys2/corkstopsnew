using Store.Application.Interfaces.Contexts;
using Store.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Posts.Queries
{
    public interface IGetProvinceServices
    {
        Task<ResultDto<List<GetProvinceDto>>> Execute();
    }

    public class GetProvinceServices : IGetProvinceServices
    {
        private readonly IDataBaseContext _context;
        public GetProvinceServices(IDataBaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto<List<GetProvinceDto>>> Execute()
        {
            var province = _context.Provinces
                 .Where(p => p.ParrentId==null)
                 .OrderBy(p => p.CityName)
                 .ToList()
                 .Select(p => new GetProvinceDto
                 {
                     Id = p.Id,
                     Name = p.CityName,
                 }).ToList();
            return new ResultDto<List<GetProvinceDto>>()
            {
                Data = province,
                IsSuccess = true,
            };

        }
    }
    public class GetProvinceDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}


