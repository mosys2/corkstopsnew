using EndPoint.Site.Areas.Admin.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Application.Interfaces.FacadPattern;
using Store.Application.Services.FileManager.Queries.ListDirectories;
using Store.Common.ResultDto;

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
    

        public async Task<IActionResult> Index()
        {
            
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _productFacad.GetAllCategoriesServices.Execute();

            ViewBag.Categories=new SelectList(categories, "Id", "Name");
            return View();
        }
      
    }
}
