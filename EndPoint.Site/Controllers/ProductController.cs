using EndPoint.Site.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces.FacadPatternSite;

namespace EndPoint.Site.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductFacadSite _productFacad;
        public ProductController(IProductFacadSite productFacad)
        {
            _productFacad= productFacad;
        }
        public async Task<IActionResult> Index(string SearchKey = "", int Page = 1, int PageSize = 20)
        {
            var result = await _productFacad.GetAllProductForSiteServices.Execute(SearchKey, Page, PageSize); 
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
          var result=await _productFacad.GetProductDetailForSiteServices.Execute(id);
            ProductSiteViewModel productSiteViewModel = new ProductSiteViewModel
            {
                product=result.Data
            };
            return View(productSiteViewModel);
        }
    }
}
