using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Constant;
using Store.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.Queries.GetProductDetailForSite
{
    
    public interface IGetProductDetailForSiteServices
    {
        Task<ResultDto<ProductDetailForSiteDto>> Execute(string IdOrSlug);
    }
    public class GetProductDetailForSiteServices : IGetProductDetailForSiteServices
    {
        private readonly IDataBaseContext _context;
        public GetProductDetailForSiteServices(IDataBaseContext context)
        {
            _context=context;
        }

        public async Task<ResultDto<ProductDetailForSiteDto>> Execute(string IdOrSlug)
        {
            try
            {
                var product = await _context.Products.Include(p => p.Category)
                                .Include(p => p.ItemTags)
                                .ThenInclude(p => p.Tag)
                                .Include(p => p.Medias)
                                .Include(p => p.Features)
                                .Include(p => p.Brand)
                                .Where(p => p.Id==IdOrSlug)
                                .Select(p => new ProductDetailForSiteDto
                                {
                                    Id=p.Id,
                                    Brand=p.Brand.Name,
                                    Category=p.Category.Name,
                                    Content= p.Content,
                                    FeatureList=p.Features.Select(p => new FeatureListEditDto
                                    {
                                        Title=p.DisplayName,
                                        Value=p.DisplayValue
                                    }).ToList(),
                                    Media=p.Medias.Select(p => PublicConst.FtpUrl+ p.Url).ToArray<string>(),
                                    MinPic=p.MinPic,
                                    Name=p.Name,
                                    Pic=p.Pic,
                                    Price=p.Price,
                                    Quantity=p.Quantity,
                                    Tags=p.ItemTags.Select(p => p.Tag.Name).ToArray<string>(),
                                })
                                .FirstOrDefaultAsync();

                return new ResultDto<ProductDetailForSiteDto>()
                {
                    Data=product,
                    IsSuccess=true
                };
            }
            catch (Exception ex)
            {
                return new ResultDto<ProductDetailForSiteDto>
                {
                    Data=null,
                    IsSuccess=false,
                    Message=ex.Message
                };
            }

        }
    }
    public class ProductDetailForSiteDto
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public string? Brand { get; set; }
        public string? Content { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public double LastPrice { get; set; }
        public int Quantity { get; set; }
        public string? Pic { get; set; }
        public string[]? Tags { get; set; }
        public string? MinPic { get; set; }
        public string[]? Media { get; set; }
        public List<FeatureListEditDto>? FeatureList { get; set; }
    }
    public class FeatureListEditDto
    {
        public string? Title { get; set; }
        public string? Value { get; set; }
    }
}
