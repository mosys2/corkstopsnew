using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.Queries.GetProductDetail
{
    public interface IGetProductDetailServices
    {
        Task<ResultDto<RequestEditProductDto>> Execute(string Id);
    }
    public class GetProductDetailServices: IGetProductDetailServices
    {
        private readonly IDataBaseContext _context;
        public GetProductDetailServices(IDataBaseContext context)
        {
            _context=context;
        }

        public async Task<ResultDto<RequestEditProductDto>> Execute(string Id)
        {
            try
            {
                var product = await _context.Products.Include(p => p.Category)
                                .Include(p => p.ItemTags)
                                .ThenInclude(p => p.Tag)
                                .Include(p => p.Medias)
                                .Include(p => p.Features)
                                .Include(p => p.Brand)
                                .Where(p => p.Id==Id)
                                .Select(p => new RequestEditProductDto
                                {
                                    Id=p.Id,
                                    BrandId=p.Brand.Id,
                                    CategoryId=p.Category.Id,
                                    Content= p.Content,
                                    FeatureList=p.Features.Select(p => new FeatureListEditDto
                                    {
                                        Title=p.DisplayName,
                                        Value=p.DisplayValue
                                    }).ToList(),
                                    IsActive=p.IsActive,
                                    Media=p.Medias.Select(p => p.Url).ToArray<string>(),
                                    MinPic=p.MinPic,
                                    Name=p.Name,
                                    Pic=p.Pic,
                                    PostageFee=p.PostageFee,
                                    PostageFeeBasedQuantity=p.PostageFeeBasedQuantity,
                                    Price=p.Price,
                                    Quantity=p.Quantity,
                                    Slug=p.Slug,
                                    TagsId=p.ItemTags.Select(p => p.TagId).ToArray<string>(),
                                })
                                .FirstOrDefaultAsync();
                                
                return new ResultDto<RequestEditProductDto>()
                {
                    Data=product,
                    IsSuccess=true
                };
            }catch(Exception ex)
            {
                return new ResultDto<RequestEditProductDto>
                {
                    Data=null,
                    IsSuccess=false,
                    Message=ex.Message
                };
            }
            
        }
    }
    public class RequestEditProductDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string CategoryId { get; set; }
        public string? BrandId { get; set; }
        public string? Content { get; set; }
        public string? Description { get; set; }
        [Required]
        public double Price { get; set; }
        public double LastPrice { get; set; }
        [Required]
        public long Quantity { get; set; }
        [Required]
        public double PostageFee { get; set; }
        [Required]
        public double PostageFeeBasedQuantity { get; set; }
        public string? Slug { get; set; }
        public bool IsActive { get; set; }
        public string? Pic { get; set; }
        public string? NameTag { get; set; }
        public string? MinPic { get; set; }
        public string[]? TagsId { get; set; }
        public string[]? Media { get; set; }
        public List<FeatureListEditDto>? FeatureList { get; set; }
    }
    public class FeatureListEditDto
    {
        public string? Title { get; set; }
        public string? Value { get; set; }
    }
}
