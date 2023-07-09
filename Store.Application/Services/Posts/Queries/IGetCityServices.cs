using Store.Application.Interfaces.Contexts;
using Store.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Posts.Queries
{
    public interface IGetCityService
    {
        ResultDto<List<GetCityDto>> Execute(string provinceId);
    }
    public class GetCityService : IGetCityService
    {
        private readonly IDataBaseContext _context;
        public GetCityService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<GetCityDto>> Execute(string provinceId)
        {

            var cities = _context.Provinces
                .Where(p => p.ParrentId == provinceId)
                .ToList()
                .Select(p => new GetCityDto
                {
                    Id=p.Id,
                    Name=p.CityName,
                    Cost=p.Cost,
                    Day=p.DeliverDay,
                    ProvinceId=p.ParrentId
                }).ToList();
            return new ResultDto<List<GetCityDto>>()
            {
                Data = cities,
                IsSuccess = true,
            };
        }
    }
    public class GetCityDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public int Day { get; set; }
        public string ProvinceId { get; set; }
    }
}

