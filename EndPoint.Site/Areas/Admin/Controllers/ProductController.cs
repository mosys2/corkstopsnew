using EndPoint.Site.Areas.Admin.Models.ContollerModels.Product;
using EndPoint.Site.Areas.Admin.Models.ViewModel;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Application.Interfaces.FacadPattern;
using Store.Application.Services.FileManager.Queries.ListDirectories;
using Store.Application.Services.Products.Commands.AddNewProduct;
using Store.Application.Services.Products.Commands.AddNewTagServices;
using Store.Application.Services.Products.Commands.EditProduct;
using Store.Application.Services.Products.Queries.GetAllBrands;
using Store.Application.Services.Products.Queries.GetAllProducts;
using Store.Application.Services.Products.Queries.GetAllTags;
using Store.Application.Services.Products.Queries.GetProductDetail;
using Store.Common.Constant;
using Store.Common.ResultDto;
using Store.Domain.Entities.Products;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductFacad _productFacad;
        public ProductController(IProductFacad productFacad)
        {
            _productFacad= productFacad;

        }

        public async Task<IActionResult> Index(string SearchKey = "", int Page = 1, int PageSize = 20)
        {
            var result=await _productFacad.GetAllProductServices.Execute(new RequestGetProductsDto()
            {
                Page=Page,
                PageSize=PageSize,
                SerachKey=SearchKey
            });
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _productFacad.GetAllCategoriesServices.Execute();
            var tags = await _productFacad.GetAllTagsServices.Execute();
            var brands=await _productFacad.GetAllBrandsServices.Execute();
            ViewBag.Categories=new SelectList(categories, "Id", "Name");
            ViewBag.Tags=new SelectList(tags, "Id", "Name");
            ViewBag.Brands=new SelectList(brands, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductModelsClass model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.ErrorsMessage.ValidationError
                });
            }
            var userId = ClaimUtility.GetUserId(User);
            if (userId == null)
            {
                return Json(new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.ErrorsMessage.LoginField
                });
            }
            var result = await _productFacad.AddNewProductServices.Execute(new RequestAddProductDto
            {
                BrandId= model.BrandId,
                CategoryId= model.CategoryId,
                Content=model.Content,
                FeatureList=model.FeatureList,
                IsActive=model.IsActive,
                Pic=model.Pic,
                Price=model.Price,
                Media=model.Media,
                MinPic=model.MinPic,
                Name=model.Name,
                Quantity=model.Quantity,
                TagsId=model.TagsId,
                PostageFee=model.PostageFee,
                PostageFeeBasedQuantity=model.PostageFeeBasedQuantity,
                Slug=model.Slug,
                UserId=userId
            });
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            var categories = await _productFacad.GetAllCategoriesServices.Execute();
            var tags = await _productFacad.GetAllTagsServices.Execute();
            var brands = await _productFacad.GetAllBrandsServices.Execute();
            ViewBag.Categories=new SelectList(categories, "Id", "Name");
            ViewBag.Tags=new SelectList(tags, "Id", "Name");
            ViewBag.Brands=new SelectList(brands, "Id", "Name");
            var product=await _productFacad.GetProductDetailServices.Execute(Id);
            return View(product.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RequestEditProductDto request)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.ErrorsMessage.ValidationError
                });
            }
            var userId = ClaimUtility.GetUserId(User);
            if (userId == null)
            {
                return Json(new ResultDto
                {
                    IsSuccess = false,
                    Message = Messages.ErrorsMessage.LoginField
                });
            }
            await _productFacad.EditProductServices.Execute(new RequestEditRegisterProductDto
            {
                Id = request.Id,
                BrandId=request.BrandId,
                CategoryId=request.CategoryId,
                Content= request.Content,
                Description= request.Description,
                FeatureList=request.FeatureList,
                IsActive=request.IsActive,
                Media=request.Media,
                MinPic=request.MinPic,
                Name=request.Name,
                Pic=request.Pic,
                PostageFee=request.PostageFee,
                PostageFeeBasedQuantity=request.PostageFeeBasedQuantity,
                Price=request.Price,
                Quantity=request.Quantity,
                Slug=request.Slug,
                TagsId=request.TagsId,
                UserId=userId

            });
            return Json(new ResultDto() { IsSuccess=true });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string itemId)
        {
           var result=await _productFacad.RemoveProductServices.Execute(itemId);
            return Json(new ResultDto { IsSuccess=result.IsSuccess,Message=result.Message});
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTags()
        {
            var tags = await _productFacad.GetAllTagsServices.Execute();
            return Json(new ResultDto<List<AllTagsDto>>(){ Data=tags,IsSuccess=true });
        }

        [HttpPost]
        public async Task<IActionResult> CreateTag(string TagName)
        {
            var result = await _productFacad.AddNewTagServices.Execute(new RequestRegisterTags_Dto { Name=TagName});
            return Json(new ResultDto()
            {
                IsSuccess= result.IsSuccess,
                Message=result.Message
            });
        }
      
    }
    public class ModelTagCreat
    {
        public string Name { get; set; }
    }
}
