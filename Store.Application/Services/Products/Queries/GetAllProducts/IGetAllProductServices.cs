using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.Queries.GetAllProducts
{
    public interface IGetAllProductServices
    {
        Task<ProducstList_Dto> Execute(RequestGetProductsDto request);
    }
    public class GetAllProductServices : IGetAllProductServices
    {
        private readonly IDataBaseContext _context;
        public GetAllProductServices(IDataBaseContext context)
        {
            _context=context;
        }
        public async Task<ProducstList_Dto> Execute(RequestGetProductsDto request)
        {
          var products= await _context.Products.Include(c=>c.Category).Select(p=>new GetProductDto()
           {
              Id=p.Id,
              IsActive=p.IsActive,
              MinPic=p.MinPic,
              Name=p.Name,
              Price=p.Price,
              Quantity=p.Quantity,
              Category=p.Category.Name
           }).AsQueryable()
           .ToListAsync();
            return new ProducstList_Dto {
                Products = products,
                CurentPage=request.Page,
                PageSize=request.PageSize,
                RowCount=products.Count
            };
        }
    }
    public class ProducstList_Dto
    {
        public int RowCount { get; set; }
        public int CurentPage { get; set; }
        public int PageSize { get; set; }
        public List<GetProductDto>? Products { get; set; }

    }
    public class GetProductDto
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? MinPic { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }
        public string Category { get; set; }
    }


    public class RequestGetProductsDto
    {
        public string? SerachKey { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
