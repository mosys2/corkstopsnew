using EndPoint.Site.Areas.Admin.Models.ContollerModels.Category;
using EndPoint.Site.Areas.Admin.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Application.Services.Categuries.Commands.RegisterCategury;
using Store.Application.Services.Categuries.Commands.RemoveCategory;
using Store.Application.Services.Categuries.Queries.GetAllCateguryForSelectList;
using Store.Common.Constant;
using Store.Common.ResultDto;
using Store.Domain.Entities.Products;
using System.Dynamic;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IGetAllCateguriesForSelectListServices _categuriesForSelectList;
        private readonly IRegisterCategoryServices _registerCategury;
        private readonly IRemoveCategoryServices _removeCategory;

        public CategoryController(IGetAllCateguriesForSelectListServices categuriesForSelectList,
            IRegisterCategoryServices registerCategury,
            IRemoveCategoryServices removeCategory)
        {
            _categuriesForSelectList= categuriesForSelectList;
            _registerCategury=registerCategury;
            _removeCategory=removeCategory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categuries =await _categuriesForSelectList.Execute();
            List<AllCateguriesSelectListDto> categuryList = new List<AllCateguriesSelectListDto>();
            categuryList.Add(new AllCateguriesSelectListDto
            {
                Id=null,
                Name="Uncategorized",
                OrginalName="Uncategorized",
                ParrentId=null,
                ParrentName="null",
                Description=""                
            });
            categuryList.AddRange(categuries);
            ViewBag.Category=new SelectList(categuryList, "Id", "Name");

            CategoryViewModel categoryView = new CategoryViewModel()
            {
                allCateguries=categuries,
                categoryModel=new CategoryModelClass()
            };
            return View(categoryView);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryModelClass model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ResultDto()
                {
                    IsSuccess=false,
                    Message=Messages.ErrorsMessage.ValidationError
                });
            }
            var result = await _registerCategury.Execute(new RequestRegisterCateguryDto
            {
                Name=model.Name,
                ParrentCategoryId=model.Category,
                IsActive=model.IsActive,
                Description=model.Description
            });
            return Json(new ResultDto()
            {
                IsSuccess=result.IsSuccess,
                Message=result.Message
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string Id)
        {
            var result = await _removeCategory.Execute(Id);
            return Json(new ResultDto()
            {
                IsSuccess=result.IsSuccess,
                Message=result.Message
            });
        }
    }
}
