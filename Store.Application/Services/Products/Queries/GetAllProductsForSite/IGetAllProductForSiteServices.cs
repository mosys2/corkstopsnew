using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Constant;
using Store.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.Queries.GetAllProductsForSite
{
    public interface IGetAllProductForSiteServices
    {
        Task<ProducstListForSite_Dto> Execute(string SerachKey,int Page,int PageSize);
    }
    public class GetAllProductForSiteServices : IGetAllProductForSiteServices
    {
        private readonly IDataBaseContext _context;
        public GetAllProductForSiteServices(IDataBaseContext context)
        {
            _context=context;
        }
        public async Task<ProducstListForSite_Dto> Execute(string SerachKey, int Page, int PageSize)
        {
            int rowsCount = 0;
            var products = _context.Products.Include(c => c.Category).AsQueryable();
            if (!string.IsNullOrEmpty(SerachKey))
            {
                products=products.Where(p => p.Name.Contains(SerachKey)).AsQueryable();
            }
            var productsList = products
                    .ToPaged(Page, PageSize, out rowsCount)
                    .Select(p => new GetProductForSiteDto()
                    {
                        Id=p.Id,
                        MinPic=string.IsNullOrEmpty(p.MinPic) ? PublicConst.NoImageUrl : PublicConst.FtpUrl+ p.MinPic,
                        Name=p.Name,
                        Description=p.Description,
                        Price=p.Price,
                        LastPrice=p.LastPrice,
                        Quantity=p.Quantity,
                        Category=p.Category?.Name
                    }).ToList();
            return new ProducstListForSite_Dto
            {
                Products = productsList,
                CurentPage=Page,
                PageSize=PageSize,
                RowCount=rowsCount
            };
        }
    }
    public class ProducstListForSite_Dto
    {
        public int RowCount { get; set; }
        public int CurentPage { get; set; }
        public int PageSize { get; set; }
        public List<GetProductForSiteDto>? Products { get; set; }

    }
    public class GetProductForSiteDto
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? MinPic { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double LastPrice { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }

    }
}
