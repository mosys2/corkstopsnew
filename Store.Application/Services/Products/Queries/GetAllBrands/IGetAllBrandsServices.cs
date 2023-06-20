using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.Queries.GetAllBrands
{
    public interface IGetAllBrandsServices
    {
        Task<List<RequestAllBrandsDto>> Execute();
    }
    public class GetAllBrandsServices : IGetAllBrandsServices
    {
        private readonly IDataBaseContext _context;
        public GetAllBrandsServices(IDataBaseContext context)
        {
            _context= context;
        }

        public async Task<List<RequestAllBrandsDto>> Execute()
        {
            var brands = new List<RequestAllBrandsDto>();
            brands.Add(new RequestAllBrandsDto { Id="", Name="No brand" });
            var brandItems = await _context.Brands.Select(p => new RequestAllBrandsDto()
            {
                Id= p.Id,
                Name=p.Name
            }).ToListAsync();
            brands.AddRange(brandItems);
            return brands;
        }
    }
    public class RequestAllBrandsDto
    {
        public string Id { get; set; }
        public string? Name { get; set; }
    }
}
