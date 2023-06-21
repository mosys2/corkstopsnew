using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Constant;
using Store.Common.Pagination;
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
            int rowsCount = 0;
            var products = _context.Products.Include(c => c.Category).AsQueryable();
            if (!string.IsNullOrEmpty(request.SerachKey))
            {
                products=products.Where(p => p.Name.Contains(request.SerachKey)).AsQueryable();
            }
            var productsList = products
                    .ToPaged(request.Page, request.PageSize, out rowsCount)
                    .Select(p => new GetProductDto()
                    {
                        Id=p.Id,
                        IsActive=p.IsActive,
                        MinPic=string.IsNullOrEmpty(p.MinPic) ? PublicConst.NoImageUrl : p.MinPic,
                        Name=p.Name,
                        Price=p.Price,
                        Quantity=p.Quantity,
                        Category=p.Category.Name
                    }).ToList();
            return new ProducstList_Dto
            {
                Products = productsList,
                CurentPage=request.Page,
                PageSize=request.PageSize,
                RowCount=rowsCount
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
        public string? Category { get; set; }
    }


    public class RequestGetProductsDto
    {
        public string? SerachKey { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
