using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Application.Services.Products.Queries.GetProductDetail;
using Store.Common.Constant;
using Store.Common.Constant.Enum;
using Store.Common.ResultDto;
using Store.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.Commands.EditProduct
{
    public interface IEditProductServices
    {
        Task<ResultDto> Execute(RequestEditRegisterProductDto request);
    }
    public class EditProductServices : IEditProductServices
    {
        private readonly IDataBaseContext _context;
        public EditProductServices(IDataBaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto> Execute(RequestEditRegisterProductDto request)
        {
            try
            {
                var product = _context.Products.Find(request.Id);
                if (product==null) { return new ResultDto { IsSuccess=false, Message="Product not found!" }; }

                var brand = await _context.Brands.FindAsync(request.BrandId);
                var category = await _context.Categories.FindAsync(request.CategoryId);
                var user = await _context.Users.FindAsync(request.UserId);
                var slug = await _context.Products.Where(s => s.Slug==request.Slug && s.Slug!=product.Slug).ToListAsync();

                if (category==null) { return new ResultDto { IsSuccess=false, Message="Please Select Category!" }; }
                if (user==null) { return new ResultDto { IsSuccess=false, Message="Please Select User!" }; }
                if (slug.Any()) { return new ResultDto { IsSuccess=false, Message="Please Change Slug!" }; }

                //Tags
                var itemTags = await _context.ItemTags.Where(p => p.ProductId==product.Id).ToListAsync();
                if (itemTags.Any())
                {
                    _context.ItemTags.RemoveRange(itemTags);
                    await _context.SaveChangesAsync();
                }

                List<ItemTag> itemTagsList = new List<ItemTag>();
                if (request.TagsId!=null)
                {
                    foreach (var item in request.TagsId)
                    {
                        var tag = await _context.Tags.FindAsync(item);
                        if (tag!=null)
                        {
                            ItemTag itemTag = new ItemTag
                            {
                                Id=Guid.NewGuid().ToString(),
                                InsertTime=DateTime.Now,
                                UpdateTime=DateTime.Now,
                                Product=product,
                                ProductId=product.Id,
                                Tag=tag,
                                TagId=tag.Id
                            };
                            itemTagsList.Add(itemTag);
                        }
                    }
                    await _context.ItemTags.AddRangeAsync(itemTagsList);
                    await _context.SaveChangesAsync();
                }

                //Feater
                var features = await _context.Features.Where(p => p.ProductId==product.Id).ToListAsync();
                if (itemTags.Any())
                {
                    _context.Features.RemoveRange(features);
                    await _context.SaveChangesAsync();
                }

                if (request.FeatureList!=null)
                {
                    List<Feature> featureList = new List<Feature>();
                    foreach (var item in request.FeatureList)
                    {
                        Feature feature = new Feature()
                        {
                            Id=Guid.NewGuid().ToString(),
                            DisplayName=item.Title,
                            DisplayValue=item.Value,
                            InsertTime=DateTime.Now,
                            UpdateTime=DateTime.Now,
                            Product=product,
                            ProductId=product.Id,
                        };
                        featureList.Add(feature);
                    }
                    await _context.Features.AddRangeAsync(featureList);
                    await _context.SaveChangesAsync();
                }

                //Mediya
                var medias = await _context.Medias.Where(p => p.ProductId==product.Id).ToListAsync();
                if (itemTags.Any())
                {
                    _context.Medias.RemoveRange(medias);
                    await _context.SaveChangesAsync();
                }

                List<Media> mediaList = new List<Media>();
                if (request.Media!=null)
                {
                    foreach (var item in request.Media)
                    {
                        Media media = new Media()
                        {
                            Id=Guid.NewGuid().ToString(),
                            InsertTime=DateTime.Now,
                            UpdateTime=DateTime.Now,
                            Product=product,
                            ProductId=product.Id,
                            Url=item,
                            MediaType=FileType.Image
                        };
                        mediaList.Add(media);
                    }
                    await _context.Medias.AddRangeAsync(mediaList);
                    await _context.SaveChangesAsync();
                }

                product.Name=request.Name;
                product.Description=request.Description;
                product.UpdateTime=DateTime.Now;
                product.Slug=request.Slug;
                product.Content=request.Content;
                product.IsActive=request.IsActive;
                product.MinPic=request.MinPic;
                product.Pic=request.Pic;
                if (product.Price!=request.Price)
                {
                    product.LastPrice=product.Price;
                    product.Price=request.Price;
                }
                product.PostageFee=request.PostageFee;
                product.PostageFeeBasedQuantity=request.PostageFeeBasedQuantity;
                product.Quantity=request.Quantity;
                product.UserId=request.UserId;

                await _context.SaveChangesAsync();
                return new ResultDto
                {
                    IsSuccess=true,
                    Message=Messages.Message.UpdateSuccess
                };
            }
            catch (Exception ex)
            {
                return new ResultDto
                {
                    IsSuccess=false,
                    Message=ex.Message,
                };
            }

        }
    }
    public class RequestEditRegisterProductDto
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string CategoryId { get; set; }
        public string? BrandId { get; set; }
        public string? Content { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double PostageFee { get; set; }
        public double PostageFeeBasedQuantity { get; set; }
        public string? Slug { get; set; }
        public string UserId { get; set; }
        public bool IsActive { get; set; }
        public string? Pic { get; set; }
        public string? MinPic { get; set; }
        public string[]? TagsId { get; set; }
        public string[]? Media { get; set; }
        public List<FeatureListEditDto>? FeatureList { get; set; }
    }

}
