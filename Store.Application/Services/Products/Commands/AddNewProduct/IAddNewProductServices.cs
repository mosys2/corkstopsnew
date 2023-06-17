using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Constant;
using Store.Common.Constant.Enum;
using Store.Common.ResultDto;
using Store.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.Commands.AddNewProduct
{
    public interface IAddNewProductServices
    {
        Task<ResultDto> Execute(RequestAddProductDto request);
    }
    public class AddNewProductServices : IAddNewProductServices
    {
        private readonly IDataBaseContext _context;
        public AddNewProductServices(IDataBaseContext context)
        {
            _context = context;
        }
        public async Task<ResultDto> Execute(RequestAddProductDto request)
        {
            try
            {
                var brand = await _context.Brands.FindAsync(request.BrandId);
                var category = await _context.Categories.FindAsync(request.CategoryId);
                var user = await _context.Users.FindAsync(request.UserId);
                var slug = await _context.Products.Where(s => s.Slug==request.Slug).ToListAsync();


                if (category==null) { return new ResultDto { IsSuccess=false, Message="Please Select Category!" }; }
                if (user==null) { return new ResultDto { IsSuccess=false, Message="Please Select User!" }; }
                if (slug.Any()) { return new ResultDto { IsSuccess=false, Message="Please Change Slug!" }; }

                //Table Product
                Product product = new Product()
                {
                    Id=Guid.NewGuid().ToString(),
                    IsActive=true,
                    InsertTime=DateTime.Now,
                    LastPrice=request.Price,
                    Name=request.Name,
                    Pic=request.Pic,
                    MinPic=request.MinPic,
                    PostageFee=request.PostageFee,
                    PostageFeeBasedQuantity=request.PostageFeeBasedQuantity,
                    Price=request.Price,
                    BrandId=brand?.Id,
                    CategoryId= category.Id,
                    Content=request.Content,
                    Quantity=request.Quantity,
                    Slug=request.Slug,
                    UserId=user.Id,
                };
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();

                //Tags
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

                //Mediya
                List<Media> mediaList = new List<Media>();
                if (request.Media!=null)
                {
                    foreach (var item in request.Media)
                    {
                        Media media = new Media()
                        {
                            Id=Guid.NewGuid().ToString(),
                            InsertTime=DateTime.Now,
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

                //Feater
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
                            Product=product,
                            ProductId=product.Id,
                        };
                        featureList.Add(feature);
                    }
                    await _context.Features.AddRangeAsync(featureList);
                    await _context.SaveChangesAsync();
                }
                return new ResultDto
                {
                    IsSuccess=true,
                    Message=Messages.Message.RegisterSuccess,
                };
            }
            catch (Exception ex)
            {
                return new ResultDto
                {
                    IsSuccess=false,
                    Message=ex.Message
                };
            }
        }

    }

    public class RequestAddProductDto
    {
        public string? Name { get; set; }
        public string CategoryId { get; set; }
        public string? BrandId { get; set; }
        public string UserId { get; set; }
        public string? Content { get; set; }
        public double Price { get; set; }
        public double LastPrice { get; set; }
        public int Quantity { get; set; }
        public double PostageFee { get; set; }
        public double PostageFeeBasedQuantity { get; set; }
        public string? Slug { get; set; }
        public bool IsActive { get; set; }
        public string? Pic { get; set; }
        public string? NameTag { get; set; }
        public string? MinPic { get; set; }
        public string[]? Media { get; set; }
        public string[]? TagsId { get; set; }
        public List<FeatureListDto>? FeatureList { get; set; }
    }
    public class FeatureListDto
    {
        public string? Title { get; set; }
        public string? Value { get; set; }
    }
}
