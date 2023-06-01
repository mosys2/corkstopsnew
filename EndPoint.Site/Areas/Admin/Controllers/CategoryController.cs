using EndPoint.Site.Areas.Admin.Models.ContollerModels.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Application.Services.Categuries.Commands.RegisterCategury;
using Store.Application.Services.Categuries.Queries.GetAllCateguryForSelectList;
using Store.Common.Constant;
using Store.Common.ResultDto;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IGetAllCateguriesForSelectListServices _categuriesForSelectList;
        private readonly IRegisterCateguryServices _registerCategury;
        public CategoryController(IGetAllCateguriesForSelectListServices categuriesForSelectList,
            IRegisterCateguryServices registerCategury)
        {
                _categuriesForSelectList= categuriesForSelectList;
            _registerCategury=registerCategury;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categuries =await _categuriesForSelectList.Execute();
            ViewBag.Categury=new SelectList(categuries, "Id", "Name");
            return View();
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

    }
}
